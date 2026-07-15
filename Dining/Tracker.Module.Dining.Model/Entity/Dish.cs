using Newtonsoft.Json;
using Tracker.Module.Dining.Abstraction.Entity;

namespace Tracker.Module.Dining.Model.Entity;

public class Dish : IDish
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

    #region Implementation of ISearchableDish

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

    #region Implementation of IDish

    /// <inheritdoc />
    public ICollection<IDinner> Dinners { get; set; }

    /// <inheritdoc />
    public ICollection<IDishIngredient> DishIngredients { get; set; }

    #endregion

    #region Constructors

    public Dish()
    {
    }

    [JsonConstructor]
    private Dish(int id)
    {
        this.id = id;
    }

    #endregion
}
