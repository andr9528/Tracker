using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Model.ComplexSearchable;

public class ComplexSearchableDinnerTemplate : IComplexSearchable<SearchableDinnerTemplate>
{
    #region Implementation of IComplexSearchable<SearchableDinnerTemplate>

    /// <inheritdoc />
    public SearchableDinnerTemplate Searchable { get; set; }

    #endregion
}
