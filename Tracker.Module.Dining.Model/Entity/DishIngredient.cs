using Newtonsoft.Json;
using Tracker.Module.Dining.Abstraction.Entity;

namespace Tracker.Module.Dining.Model.Entity;

public class DishIngredient : IDishIngredient
{
    #region Fields

    private readonly int id;

    #endregion

    #region Implementation of ISearchable

    /// <inheritdoc />
    public int Id
    {
        get => id;
        set => throw new InvalidOperationException($"{nameof(Id)} cannot be changed");
    }

    #endregion

    #region Implementation of ISearchableDishIngredient

    /// <inheritdoc />
    public int DishId { get; set; }

    /// <inheritdoc />
    public int IngredientId { get; set; }

    #endregion

    #region Implementation of IEntity

    /// <inheritdoc />
    public byte[] Version { get; set; }

    /// <inheritdoc />
    public DateTime CreatedDateTime { get; set; }

    /// <inheritdoc />
    public DateTime UpdatedDateTime { get; set; }

    #endregion

    #region Implementation of IDishIngredient

    /// <inheritdoc />
    public IDish Dish { get; set; }

    /// <inheritdoc />
    public IIngredient Ingredient { get; set; }

    #endregion

    #region Constructors

    public DishIngredient()
    {
    }

    [JsonConstructor]
    private DishIngredient(int id)
    {
        this.id = id;
    }

    #endregion
}
