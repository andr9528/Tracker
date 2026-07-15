using Newtonsoft.Json;
using Tracker.Module.Dining.Abstraction.Entity;

namespace Tracker.Module.Dining.Model.Entity;

public class Ingredient : IIngredient
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

    #region Implementation of ISearchableIngredient

    /// <inheritdoc />
    public string Name { get; set; }

    #endregion

    #region Implementation of IEntity

    /// <inheritdoc />
    public byte[] Version { get; set; }

    /// <inheritdoc />
    public DateTime CreatedDateTime { get; set; }

    /// <inheritdoc />
    public DateTime UpdatedDateTime { get; set; }

    #endregion

    #region Implementation of IIngredient

    /// <inheritdoc />
    public ICollection<IDishIngredient> DishIngredients { get; set; }

    /// <inheritdoc />
    public bool InStock { get; set; }

    #endregion

    #region Constructors

    public Ingredient()
    {
    }

    [JsonConstructor]
    private Ingredient(int id)
    {
        this.id = id;
    }

    #endregion
}
