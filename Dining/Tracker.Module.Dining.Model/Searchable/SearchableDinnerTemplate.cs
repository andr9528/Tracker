using Tracker.Module.Dining.Abstraction.Searchable;

namespace Tracker.Module.Dining.Model.Searchable;

public class SearchableDinnerTemplate : ISearchableDinnerTemplate
{
    #region Implementation of ISearchable

    /// <inheritdoc />
    public int Id { get; set; }

    #endregion

    #region Implementation of ISearchableDinnerTemplate

    /// <inheritdoc />
    public string Name { get; set; }

    #endregion
}
