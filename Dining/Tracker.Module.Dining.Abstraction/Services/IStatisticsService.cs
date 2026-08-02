using Tracker.Module.Dining.Abstraction.Records;

namespace Tracker.Module.Dining.Abstraction.Services;

public interface IStatisticsService
{
    /// <summary>
    /// Gets the dish with the most registered dinners.
    /// If several dishes have the same number of dinners,
    /// the most recently eaten dish is returned.
    /// </summary>
    Task<DishEatingStatistic?> GetMostEatenDish();

    /// <summary>
    /// Gets the dish with the fewest registered dinners.
    /// If several dishes have the same number of dinners,
    /// the dish eaten least recently is returned.
    /// </summary>
    Task<DishEatingStatistic?> GetLeastEatenDish();

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
}
