using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Model.ComplexSearchable;

public class ComplexSearchableDinnerTemplate : IComplexSearchable<SearchableDinnerTemplate>
{
    #region Implementation of IComplexSearchable<SearchableDinnerTemplate>

    /// <inheritdoc />
    public SearchableDinnerTemplate Searchable { get; set; } = new();

    public string? Name { get; set; }

    public bool? IsTakeAway { get; set; }
    public bool? HasLeftovers { get; set; }
    public bool? LeftoversEnoughForDinner { get; set; }
    public bool? IsLeftovers { get; set; }
    public bool? IsEatenOut { get; set; }
    public bool? IsReadyMadeDish { get; set; }

    #endregion
}
