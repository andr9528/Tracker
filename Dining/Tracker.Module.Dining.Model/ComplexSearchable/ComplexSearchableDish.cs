using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Model.ComplexSearchable;

public class ComplexSearchableDish : IComplexSearchable<SearchableDish>
{
    public string? Name { get; set; }
    public string? Ingredients { get; set; }

    /// <inheritdoc />
    public SearchableDish Searchable { get; set; } = new();
}
