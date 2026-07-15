using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Model.ComplexSearchable;

public class ComplexSearchableIngredient : IComplexSearchable<SearchableIngredient>
{
    /// <inheritdoc />
    public SearchableIngredient Searchable { get; set; } = new();

    public string? Name { get; set; }
    public bool? InStock { get; set; }
}
