namespace Tracker.Module.Dining.Abstraction.Records;

public sealed class ImportResult
{
    public int CreatedDinners { get; set; }

    public int SkippedExistingDinners { get; set; }

    public int SkippedInvalidRows { get; set; }

    public int CreatedDishes { get; set; }

    public int CreatedIngredients { get; set; }

    public int CreatedDishIngredients { get; set; }
};
