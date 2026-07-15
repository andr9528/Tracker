using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Model.ComplexSearchable;

public class ComplexSearchableDinner : IComplexSearchable<SearchableDinner>
{
    /// <inheritdoc />
    public SearchableDinner Searchable { get; set; } = new();

    public string? Notes { get; set; }

    public bool? IsTakeAway { get; set; }
    public bool? HasLeftovers { get; set; }
    public bool? LeftoversEnoughForDinner { get; set; }
    public bool? IsLeftovers { get; set; }
}
