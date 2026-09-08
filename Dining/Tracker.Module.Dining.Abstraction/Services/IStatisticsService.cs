using Tracker.Module.Dining.Abstraction.Records;

namespace Tracker.Module.Dining.Abstraction.Services;

public interface IStatisticsService
{
    /// <summary>
    /// Gets the dishes with the most registered dinners.
    /// If several dishes have the same number of dinners,
    /// the most recently eaten dish is returned.
    /// </summary>
    Task<IReadOnlyCollection<DishEatingStatistic>> GetMostEatenDishes();

    /// <summary>
    /// Gets the dishes with the fewest registered dinners.
    /// If several dishes have the same number of dinners,
    /// the dish eaten least recently is returned.
    /// </summary>
    Task<IReadOnlyCollection<DishEatingStatistic>> GetLeastEatenDishes();

    /// <summary>
    /// Gets the number of registered dishes.
    /// </summary>
    Task<int> GetUniqueDishesEaten();

    /// <summary>
    /// Gets the three ingredients used across the greatest number of dinners.
    /// </summary>
    Task<IReadOnlyCollection<IngredientUsageStatistic>> GetMostUsedIngredients();

    /// <summary>
    /// Gets the dish most recently added to the database.
    /// </summary>
    Task<RecentlyAddedDishStatistic?> GetMostRecentlyAddedDish();

    /// <summary>
    /// Gets the Average amount of days between eating each dish.
    /// </summary>
    /// <returns></returns>
    Task<IReadOnlyDictionary<int, float?>> GetDishAverageDaysBetweenEating();

    /// <summary>
    /// Gets the Average amount of days between using each ingredient.
    /// </summary>
    /// <returns></returns>
    Task<IReadOnlyDictionary<int, float?>> GetIngredientAverageDaysBetweenUsage();

    /// <summary>
    /// Clears any cached data.
    /// </summary>
    void Invalidate();
}
