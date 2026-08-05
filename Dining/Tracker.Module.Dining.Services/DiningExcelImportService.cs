using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Abstraction.Records;
using Tracker.Module.Dining.Abstraction.Services;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Services.Import;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Services;

public sealed class DiningExcelImportService : IDiningImportService
{
    private readonly DiningSpreadsheetReader spreadsheetReader;

    private readonly IEntityQueryService<Dinner, SearchableDinner> dinnerQueryService;

    private readonly IEntityQueryService<Dish, SearchableDish> dishQueryService;

    private readonly IEntityQueryService<Ingredient, SearchableIngredient> ingredientQueryService;

    private readonly IEntityQueryService<DishIngredient, SearchableDishIngredient> dishIngredientQueryService;

    private readonly ILogger<DiningExcelImportService> logger;

    private readonly Dictionary<DishImportKey, Dish> dishes = [];

    private readonly Dictionary<string, Ingredient> ingredients = new(StringComparer.OrdinalIgnoreCase);

    private readonly HashSet<DishIngredientKey> dishIngredients = [];

    public DiningExcelImportService(
        DiningSpreadsheetReader spreadsheetReader, IEntityQueryService<Dinner, SearchableDinner> dinnerQueryService,
        IEntityQueryService<Dish, SearchableDish> dishQueryService,
        IEntityQueryService<Ingredient, SearchableIngredient> ingredientQueryService,
        IEntityQueryService<DishIngredient, SearchableDishIngredient> dishIngredientQueryService,
        ILogger<DiningExcelImportService> logger)
    {
        this.spreadsheetReader = spreadsheetReader;
        this.dinnerQueryService = dinnerQueryService;
        this.dishQueryService = dishQueryService;
        this.ingredientQueryService = ingredientQueryService;
        this.dishIngredientQueryService = dishIngredientQueryService;
        this.logger = logger;
    }

    private async Task<ImportResult> Import(Stream stream)
    {
        ClearImportCaches();
        await LoadImportCaches();

        var counters = new ImportResult();
        var rows = spreadsheetReader.Read(stream, counters);
        var existingDinners = await GetExistingDinners();
        var createdDinners = new List<Dinner>();
        var updatedDinners = new List<Dinner>();

        foreach (DiningSpreadsheetRow row in rows)
        {
            await ImportDinner(row, existingDinners, createdDinners, updatedDinners, counters);
        }

        await SaveDinners(createdDinners, updatedDinners);

        return counters;
    }

    private async Task<Dictionary<DateOnly, Dinner>> GetExistingDinners()
    {
        var existingDinners = await dinnerQueryService.GetAllEntities();

        return existingDinners.ToDictionary(x => x.Date);
    }

    private async Task LoadImportCaches()
    {
        var existingIngredients = await ingredientQueryService.GetAllEntities();
        var existingDishes = await dishQueryService.GetAllEntities();
        var existingDishIngredients = await dishIngredientQueryService.GetAllEntities();

        CacheIngredients(existingIngredients);
        CacheDishIngredients(existingDishIngredients);
        CacheDishes(existingDishes, existingDishIngredients);
    }

    private void CacheIngredients(IEnumerable<Ingredient> existingIngredients)
    {
        foreach (Ingredient ingredient in existingIngredients)
        {
            ingredients.TryAdd(NormalizeName(ingredient.Name), ingredient);
        }
    }

    private void CacheDishIngredients(IEnumerable<DishIngredient> existingDishIngredients)
    {
        foreach (DishIngredient dishIngredient in existingDishIngredients)
        {
            dishIngredients.Add(new DishIngredientKey(dishIngredient.DishId, dishIngredient.IngredientId));
        }
    }

    private void CacheDishes(IEnumerable<Dish> existingDishes, IEnumerable<DishIngredient> existingDishIngredients)
    {
        var ingredientIdsByDish = existingDishIngredients.GroupBy(x => x.DishId)
            .ToDictionary(x => x.Key, x => x.Select(y => y.IngredientId).ToList());

        foreach (Dish dish in existingDishes)
        {
            IReadOnlyCollection<int> ingredientIds =
                ingredientIdsByDish.TryGetValue(dish.Id, out var foundIngredientIds) ? foundIngredientIds : [];

            DishImportKey key = CreateDishImportKey(dish.Name, ingredientIds);

            dishes.TryAdd(key, dish);
        }
    }

    private async Task ImportDinner(
        DiningSpreadsheetRow row, IDictionary<DateOnly, Dinner> existingDinners, ICollection<Dinner> createdDinners,
        ICollection<Dinner> updatedDinners, ImportResult counters)
    {
        var rowIngredients = await FindOrCreateIngredients(row.IngredientNames, counters);

        Dish dish = await FindOrCreateDish(row.DishName, rowIngredients, counters);

        if (existingDinners.TryGetValue(row.Date, out Dinner? existingDinner))
        {
            UpdateDinnerEntity(existingDinner, row, dish);
            updatedDinners.Add(existingDinner);
            counters.UpdatedDinners++;

            return;
        }

        Dinner dinner = CreateDinnerEntity(row, dish);

        existingDinners.Add(row.Date, dinner);
        createdDinners.Add(dinner);
        counters.CreatedDinners++;
    }

    private async Task<IReadOnlyCollection<Ingredient>> FindOrCreateIngredients(
        IEnumerable<string> ingredientNames, ImportResult counters)
    {
        var foundIngredients = new List<Ingredient>();

        foreach (string ingredientName in ingredientNames)
        {
            Ingredient ingredient = await FindOrCreateIngredient(ingredientName, counters);

            foundIngredients.Add(ingredient);
        }

        return foundIngredients;
    }

    private async Task<Dish> FindOrCreateDish(
        string dishName, IReadOnlyCollection<Ingredient> rowIngredients, ImportResult counters)
    {
        string normalizedName = NormalizeName(dishName);

        DishImportKey key = CreateDishImportKey(normalizedName, rowIngredients.Select(x => x.Id));

        if (dishes.TryGetValue(key, out Dish? existingDish))
        {
            return existingDish;
        }

        return await CreateDish(normalizedName, rowIngredients, key, counters);
    }

    private async Task<Dish> CreateDish(
        string dishName, IReadOnlyCollection<Ingredient> rowIngredients, DishImportKey key, ImportResult counters)
    {
        var dish = new Dish
        {
            Name = dishName,
            Dinners = [],
            DishIngredients = [],
        };

        await dishQueryService.AddEntity(dish);
        await ConnectIngredients(dish, rowIngredients, counters);

        dishes.Add(key, dish);
        counters.CreatedDishes++;

        return dish;
    }

    private async Task ConnectIngredients(Dish dish, IEnumerable<Ingredient> rowIngredients, ImportResult counters)
    {
        foreach (Ingredient ingredient in rowIngredients)
        {
            await FindOrCreateDishIngredient(dish, ingredient, counters);
        }
    }

    private async Task<Ingredient> FindOrCreateIngredient(string ingredientName, ImportResult counters)
    {
        string normalizedName = NormalizeName(ingredientName);

        if (ingredients.TryGetValue(normalizedName, out Ingredient? existingIngredient))
        {
            return existingIngredient;
        }

        return await CreateIngredient(normalizedName, counters);
    }

    private async Task<Ingredient> CreateIngredient(string ingredientName, ImportResult counters)
    {
        var ingredient = new Ingredient
        {
            Name = ingredientName,
            InStock = false,
            DishIngredients = [],
        };

        await ingredientQueryService.AddEntity(ingredient);

        ingredients.Add(ingredientName, ingredient);
        counters.CreatedIngredients++;

        return ingredient;
    }

    private async Task FindOrCreateDishIngredient(Dish dish, Ingredient ingredient, ImportResult counters)
    {
        var key = new DishIngredientKey(dish.Id, ingredient.Id);

        if (dishIngredients.Contains(key))
        {
            return;
        }

        await CreateDishIngredient(dish, ingredient, key, counters);
    }

    private async Task CreateDishIngredient(
        Dish dish, Ingredient ingredient, DishIngredientKey key, ImportResult counters)
    {
        var dishIngredient = new DishIngredient
        {
            DishId = dish.Id,
            Dish = dish,
            IngredientId = ingredient.Id,
            Ingredient = ingredient,
        };

        await dishIngredientQueryService.AddEntity(dishIngredient);

        dishIngredients.Add(key);
        counters.CreatedDishIngredients++;
    }

    private async Task SaveDinners(
        IReadOnlyCollection<Dinner> createdDinners, IReadOnlyCollection<Dinner> updatedDinners)
    {
        if (createdDinners.Count > 0)
        {
            await dinnerQueryService.AddEntities(createdDinners);
        }

        if (updatedDinners.Count > 0)
        {
            await dinnerQueryService.UpdateEntities(updatedDinners);
        }
    }

    private Dinner CreateDinnerEntity(DiningSpreadsheetRow row, Dish dish)
    {
        var dinner = new Dinner
        {
            Date = row.Date,
        };

        ApplySpreadsheetValues(dinner, row, dish);

        return dinner;
    }

    private void UpdateDinnerEntity(Dinner dinner, DiningSpreadsheetRow row, Dish dish)
    {
        ApplySpreadsheetValues(dinner, row, dish);
    }

    private void ApplySpreadsheetValues(Dinner dinner, DiningSpreadsheetRow row, Dish dish)
    {
        dinner.DishId = dish.Id;
        dinner.Dish = dish;
        dinner.Notes = row.Notes;
        dinner.IsEatenOut = row.IsEatenOut;
        dinner.IsReadyMadeDish = row.IsReadyMadeDish;
        dinner.IsTakeAway = row.IsTakeAway;
        dinner.HasLeftovers = row.HasLeftovers;
        dinner.LeftoversEnoughForDinner = row.LeftoversEnoughForDinner;
        dinner.IsLeftovers = row.IsLeftovers;
    }

    private void ClearImportCaches()
    {
        dishes.Clear();
        ingredients.Clear();
        dishIngredients.Clear();
    }

    private string NormalizeName(string name)
    {
        return name.Trim();
    }

    private DishImportKey CreateDishImportKey(string dishName, IEnumerable<int> ingredientIds)
    {
        string normalizedName = NormalizeName(dishName).ToUpperInvariant();

        string normalizedIngredientIds = string.Join(',', ingredientIds.Distinct().Order());

        return new DishImportKey(normalizedName, normalizedIngredientIds);
    }

    #region Implementation of IDiningImportService

    /// <inheritdoc />
    public async Task<ImportResult> Import(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!File.Exists(filePath))
        {
            logger.LogWarning("Unable to import dining data. The file '{FilePath}' does not exist.", filePath);

            throw new FileNotFoundException("The supplied file could not be found.", filePath);
        }

        string extension = Path.GetExtension(filePath);

        if (!string.Equals(extension, ".xlsx", StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(extension, ".xls", StringComparison.OrdinalIgnoreCase))
        {
            logger.LogWarning("Unable to import dining data. The file '{FilePath}' is not an Excel file.", filePath);

            throw new InvalidOperationException($"The file '{filePath}' is not an Excel file.");
        }

        logger.LogInformation("Importing dining data from '{FilePath}'.", filePath);

        await using FileStream stream = File.OpenRead(filePath);

        ImportResult result = await Import(stream);

        logger.LogInformation(
            "Imported {CreatedDinners} new dinners, updated {UpdatedDinners} existing dinners, created {CreatedDishes} dishes, {CreatedIngredients} ingredients and {CreatedDishIngredients} dish ingredient relations.",
            result.CreatedDinners, result.UpdatedDinners, result.CreatedDishes, result.CreatedIngredients,
            result.CreatedDishIngredients);

        return result;
    }

    #endregion

    private readonly record struct DishIngredientKey(int DishId, int IngredientId);

    private readonly record struct DishImportKey(string DishName, string IngredientIds);
}
