using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Abstraction.Records;
using Tracker.Module.Dining.Abstraction.Services;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Persistence.Query;
using Tracker.Module.Dining.Services.Import;

namespace Tracker.Module.Dining.Services;

public sealed class DiningExcelImportService : IDiningImportService
{
    private readonly DiningSpreadsheetReader spreadsheetReader;
    private readonly DinnerQueryService dinnerQueryService;
    private readonly DishQueryService dishQueryService;
    private readonly IngredientQueryService ingredientQueryService;
    private readonly DishIngredientQueryService dishIngredientQueryService;
    private readonly ILogger<DiningExcelImportService> logger;

    private readonly Dictionary<string, Dish> dishes = new(StringComparer.OrdinalIgnoreCase);

    private readonly Dictionary<string, Ingredient> ingredients = new(StringComparer.OrdinalIgnoreCase);

    private readonly HashSet<DishIngredientKey> dishIngredients = [];

    public DiningExcelImportService(
        DiningSpreadsheetReader spreadsheetReader, DinnerQueryService dinnerQueryService,
        DishQueryService dishQueryService, IngredientQueryService ingredientQueryService,
        DishIngredientQueryService dishIngredientQueryService, ILogger<DiningExcelImportService> logger)
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

        var counters = new ImportResult();
        var rows = spreadsheetReader.Read(stream, counters);
        var existingDates = await GetExistingDinnerDates();
        var dinners = new List<Dinner>();

        foreach (DiningSpreadsheetRow row in rows)
        {
            Dinner? dinner = await CreateDinner(row, existingDates, counters);

            if (dinner is not null)
            {
                dinners.Add(dinner);
            }
        }

        await AddDinners(dinners);

        return counters;
    }

    private async Task<HashSet<DateOnly>> GetExistingDinnerDates()
    {
        var existingDinners = await dinnerQueryService.GetAllEntities();

        return existingDinners.Select(x => x.Date).ToHashSet();
    }

    private async Task<Dinner?> CreateDinner(
        DiningSpreadsheetRow row, HashSet<DateOnly> existingDates, ImportResult counters)
    {
        if (!existingDates.Add(row.Date))
        {
            LogExistingDinner(row);
            counters.SkippedExistingDinners++;

            return null;
        }

        Dish dish = await FindOrCreateDish(row.DishName, counters);

        await ConnectIngredients(dish, row.IngredientNames, counters);

        counters.CreatedDinners++;

        return CreateDinnerEntity(row, dish);
    }

    private async Task<Dish> FindOrCreateDish(string dishName, ImportResult counters)
    {
        string normalizedName = NormalizeName(dishName);

        if (dishes.TryGetValue(normalizedName, out Dish? cachedDish))
        {
            return cachedDish;
        }

        Dish? existingDish = await FindDish(normalizedName);

        if (existingDish is not null)
        {
            dishes.Add(normalizedName, existingDish);

            return existingDish;
        }

        return await CreateDish(normalizedName, counters);
    }

    private async Task<Dish?> FindDish(string dishName)
    {
        var searchable = new ComplexSearchableDish
        {
            Searchable = new SearchableDish
            {
                Name = dishName,
            },
        };

        return await dishQueryService.GetEntityComplex(searchable);
    }

    private async Task<Dish> CreateDish(string dishName, ImportResult counters)
    {
        var dish = new Dish
        {
            Name = dishName,
            Dinners = [],
            DishIngredients = [],
        };

        await dishQueryService.AddEntity(dish);

        dishes.Add(dishName, dish);
        counters.CreatedDishes++;

        return dish;
    }

    private async Task ConnectIngredients(Dish dish, IEnumerable<string> ingredientNames, ImportResult counters)
    {
        foreach (string ingredientName in ingredientNames)
        {
            Ingredient ingredient = await FindOrCreateIngredient(ingredientName, counters);

            await FindOrCreateDishIngredient(dish, ingredient, counters);
        }
    }

    private async Task<Ingredient> FindOrCreateIngredient(string ingredientName, ImportResult counters)
    {
        string normalizedName = NormalizeName(ingredientName);

        if (ingredients.TryGetValue(normalizedName, out Ingredient? cachedIngredient))
        {
            return cachedIngredient;
        }

        Ingredient? existingIngredient = await FindIngredient(normalizedName);

        if (existingIngredient is not null)
        {
            ingredients.Add(normalizedName, existingIngredient);

            return existingIngredient;
        }

        return await CreateIngredient(normalizedName, counters);
    }

    private async Task<Ingredient?> FindIngredient(string ingredientName)
    {
        var searchable = new ComplexSearchableIngredient
        {
            Searchable = new SearchableIngredient
            {
                Name = ingredientName,
            },
        };

        return await ingredientQueryService.GetEntityComplex(searchable);
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

        DishIngredient? existing = await FindDishIngredient(dish, ingredient);

        if (existing is not null)
        {
            dishIngredients.Add(key);
            return;
        }

        await CreateDishIngredient(dish, ingredient, key, counters);
    }

    private async Task<DishIngredient?> FindDishIngredient(Dish dish, Ingredient ingredient)
    {
        var searchable = new ComplexSearchableDishIngredient
        {
            Searchable = new SearchableDishIngredient
            {
                DishId = dish.Id,
                IngredientId = ingredient.Id,
            },
        };

        return await dishIngredientQueryService.GetEntityComplex(searchable);
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

    private async Task AddDinners(IReadOnlyCollection<Dinner> dinners)
    {
        if (dinners.Count == 0)
        {
            return;
        }

        await dinnerQueryService.AddEntities(dinners);
    }

    private Dinner CreateDinnerEntity(DiningSpreadsheetRow row, Dish dish)
    {
        return new Dinner
        {
            Date = row.Date,
            DishId = dish.Id,
            Dish = dish,
            Notes = row.Notes,
            IsTakeAway = row.IsTakeAway,
            HasLeftovers = row.HasLeftovers,
            LeftoversEnoughForDinner = row.LeftoversEnoughForDinner,
            IsLeftovers = row.IsLeftovers,
        };
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

    private void LogExistingDinner(DiningSpreadsheetRow row)
    {
        logger.LogWarning("Skipping spreadsheet row {RowNumber}. " + "A dinner already exists for {DinnerDate}.",
            row.RowNumber, row.Date);
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
            "Imported {CreatedDinners} dinners, {CreatedDishes} dishes, {CreatedIngredients} ingredients and {CreatedDishIngredients} dish ingredient relations.",
            result.CreatedDinners, result.CreatedDishes, result.CreatedIngredients, result.CreatedDishIngredients);

        return result;
    }

    #endregion

    private readonly record struct DishIngredientKey(int DishId, int IngredientId);
}
