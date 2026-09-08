using Tracker.Module.Dining.Abstraction.Records;
using Tracker.Module.Dining.Abstraction.Services;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Services;

public sealed class StatisticsService(
    IEntityQueryService<Dish, SearchableDish> dishQueryService,
    IEntityQueryService<Ingredient, SearchableIngredient> ingredientQueryService,
    TimeProvider timeProvider) : IStatisticsService
{
    private IReadOnlyDictionary<int, float?>? dishAverageDays;
    private IReadOnlyDictionary<int, float?>? ingredientAverageDays;

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<DishEatingStatistic>> GetMostEatenDishes()
    {
        var statistics = await GetDishEatingStatistics();

        return statistics.OrderByDescending(x => x.DinnerCount).ThenByDescending(x => x.LastEaten).Take(3).ToArray();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<DishEatingStatistic>> GetLeastEatenDishes()
    {
        var statistics = await GetDishEatingStatistics();

        return statistics.OrderBy(x => x.DinnerCount).ThenBy(x => x.LastEaten).Take(3).ToArray();
    }

    /// <inheritdoc />
    public async Task<int> GetUniqueDishesEaten()
    {
        var dishes = await dishQueryService.GetAllEntities();

        return dishes.Count();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<IngredientUsageStatistic>> GetMostUsedIngredients()
    {
        var ingredients = await ingredientQueryService.GetAllEntities();

        return ingredients.Select(CreateIngredientUsageStatistic).Where(x => x.DinnerCount != 0)
            .OrderByDescending(x => x.DinnerCount).ThenByDescending(x => x.DishCount).ThenBy(x => x.IngredientName)
            .Take(3).ToArray();
    }

    public async Task<RecentlyAddedDishStatistic?> GetMostRecentlyAddedDish()
    {
        var dishes = await dishQueryService.GetAllEntities();

        Dish? dish = dishes.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

        return dish is null ? null : new RecentlyAddedDishStatistic(dish.Id, dish.Name, dish.CreatedDateTime);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyDictionary<int, float?>> GetDishAverageDaysBetweenEating()
    {
        return dishAverageDays ??= await CreateDishAverageDays();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyDictionary<int, float?>> GetIngredientAverageDaysBetweenUsage()
    {
        return ingredientAverageDays ??= await CreateIngredientAverageDays();
    }

    /// <inheritdoc />
    public void Invalidate()
    {
        dishAverageDays = null;
        ingredientAverageDays = null;
    }

    private async Task<IReadOnlyDictionary<int, float?>> CreateDishAverageDays()
    {
        var dishes = await dishQueryService.GetAllEntities();

        return dishes.ToDictionary(x => x.Id,
            x => CalculateAverageDaysBetween(
                x.Dinners.Where(dinner => !dinner.IsEatenOut).Select(dinner => dinner.Date)));
    }

    private async Task<IReadOnlyDictionary<int, float?>> CreateIngredientAverageDays()
    {
        var ingredients = await ingredientQueryService.GetAllEntities();

        return ingredients.ToDictionary(x => x.Id,
            x => CalculateAverageDaysBetween(x.DishIngredients.SelectMany(dishIngredient => dishIngredient.Dish.Dinners)
                .Where(dinner => !dinner.IsEatenOut).Select(dinner => dinner.Date)));
    }

    private async Task<IReadOnlyCollection<DishEatingStatistic>> GetDishEatingStatistics()
    {
        var dishes = await dishQueryService.GetAllEntities();

        return dishes.Select(CreateDishEatingStatistic).Where(x => x is not null).Cast<DishEatingStatistic>().ToArray();
    }

    private DishEatingStatistic? CreateDishEatingStatistic(Dish dish)
    {
        var includedDinners = dish.Dinners.Where(x => !x.IsEatenOut).ToArray();

        return includedDinners.Length == 0
            ? null
            : new DishEatingStatistic(dish.Id, dish.Name, includedDinners.Length, includedDinners.Max(x => x.Date));
    }

    private IngredientUsageStatistic CreateIngredientUsageStatistic(Ingredient ingredient)
    {
        int dinnerCount = ingredient.DishIngredients.Sum(x => x.Dish.Dinners.Count(dinner => !dinner.IsEatenOut));

        return new IngredientUsageStatistic(ingredient.Id, ingredient.Name, dinnerCount,
            ingredient.DishIngredients.Count);
    }

    private float? CalculateAverageDaysBetween(IEnumerable<DateOnly> dates)
    {
        var orderedDates = dates.OrderBy(x => x).ToList();

        if (orderedDates.Count == 0)
        {
            return null;
        }

        orderedDates.Add(DateOnly.FromDateTime(timeProvider.GetLocalNow().DateTime));

        return (float) orderedDates
            .Zip(orderedDates.Skip(1), (previous, current) => current.DayNumber - previous.DayNumber).Average();
    }
}
