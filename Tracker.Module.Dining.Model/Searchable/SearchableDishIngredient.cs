using Tracker.Module.Dining.Abstraction.Searchable;

namespace Tracker.Module.Dining.Model.Searchable;

public class SearchableDishIngredient : ISearchableDishIngredient
{
    #region Implementation of ISearchable

    /// <inheritdoc />
    public int Id { get; set; }

    #endregion

    #region Implementation of ISearchableDishIngredient

    /// <inheritdoc />
    public int DishId { get; set; }

    /// <inheritdoc />
    public int IngredientId { get; set; }

    #endregion
}
