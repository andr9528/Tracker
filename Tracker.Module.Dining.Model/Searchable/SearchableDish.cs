using Tracker.Module.Dining.Abstraction.Searchable;

namespace Tracker.Module.Dining.Model.Searchable;

public class SearchableDish : ISearchableDish
{
    #region Implementation of ISearchable

    /// <inheritdoc />
    public int Id { get; set; }

    #endregion

    #region Implementation of ISearchableDish

    /// <inheritdoc />
    public string Name { get; set; }

    #endregion
}
