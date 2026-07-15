using Tracker.Module.Dining.Abstraction.Searchable;

namespace Tracker.Module.Dining.Model.Searchable;

public class SearchableIngredient : ISearchableIngredient
{
    #region Implementation of ISearchable

    /// <inheritdoc />
    public int Id { get; set; }

    #endregion

    #region Implementation of ISearchableIngredient

    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public bool InStock { get; set; }

    #endregion
}
