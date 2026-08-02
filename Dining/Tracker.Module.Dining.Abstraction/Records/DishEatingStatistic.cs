namespace Tracker.Module.Dining.Abstraction.Records;

public sealed record DishEatingStatistic(int DishId, string DishName, int DinnerCount, DateOnly LastEaten);
