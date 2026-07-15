using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Model.ComplexSearchable;

public class ComplexSearchableDishIngredient : IComplexSearchable<SearchableDishIngredient>
{
    /// <inheritdoc />
    public SearchableDishIngredient Searchable { get; set; } = new();
}
