namespace Tracker.Module.Dining.Abstraction.Records;

public sealed record DishEatingStatistic(int DishId, string DishName, int DinnerCount, DateOnly LastEaten)
{
    public string Details
    {
        get
        {
            string dinnerText = DinnerCount == 1 ? "1 dinner" : $"{DinnerCount} dinners";

            return $"{dinnerText} · Last eaten {LastEaten:dd-MM-yyyy}";
        }
    }
}
