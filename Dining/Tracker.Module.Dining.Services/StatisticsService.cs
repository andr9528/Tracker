using Tracker.Module.Dining.Abstraction.Records;
using Tracker.Module.Dining.Abstraction.Services;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Services;

public sealed class StatisticsService(
    IEntityQueryService<Dish, SearchableDish> dishQueryService,
    IEntityQueryService<Ingredient, SearchableIngredient> ingredientQueryService) : IStatisticsService
{
    /// <inheritdoc />
    public async Task<DishEatingStatistic?> GetMostEatenDish()
    {
        var statistics = await GetDishEatingStatistics();

        return statistics.OrderByDescending(x => x.DinnerCount).ThenByDescending(x => x.LastEaten).FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<DishEatingStatistic?> GetLeastEatenDish()
    {
        var statistics = await GetDishEatingStatistics();

        return statistics.OrderBy(x => x.DinnerCount).ThenBy(x => x.LastEaten).FirstOrDefault();
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

    private async Task<IReadOnlyCollection<DishEatingStatistic>> GetDishEatingStatistics()
    {
        var dishes = await dishQueryService.GetAllEntities();

        return dishes.Where(x => x.Dinners.Count != 0).Select(CreateDishEatingStatistic).ToArray();
    }

    private DishEatingStatistic CreateDishEatingStatistic(Dish dish)
    {
        return new DishEatingStatistic(dish.Id, dish.Name, dish.Dinners.Count, dish.Dinners.Max(x => x.Date));
    }

    private IngredientUsageStatistic CreateIngredientUsageStatistic(Ingredient ingredient)
    {
        int dinnerCount = ingredient.DishIngredients.Sum(x => x.Dish.Dinners.Count);

        return new IngredientUsageStatistic(ingredient.Id, ingredient.Name, dinnerCount,
            ingredient.DishIngredients.Count);
    }
}
