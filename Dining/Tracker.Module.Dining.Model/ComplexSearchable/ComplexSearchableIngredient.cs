using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Model.ComplexSearchable;

public class ComplexSearchableIngredient : IComplexSearchable<SearchableIngredient>
{
    public string? Name { get; set; }
    public bool? InStock { get; set; }

    public int? MinimumDishCount { get; set; }
    public int? MaximumDishCount { get; set; }

    /// <inheritdoc />
    public SearchableIngredient Searchable { get; set; } = new();
}
