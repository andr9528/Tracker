using Newtonsoft.Json;
using Tracker.Module.Dining.Abstraction.Entity;

namespace Tracker.Module.Dining.Model.Entity;

public class Dinner : IDinner
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

    #region Implementation of ISearchableDinner

    /// <inheritdoc />
    public DateOnly Date { get; set; }

    /// <inheritdoc />
    public string? Notes { get; set; }

    /// <inheritdoc />
    public bool IsTakeAway { get; set; }

    /// <inheritdoc />
    public bool HasLeftovers { get; set; }

    /// <inheritdoc />
    public bool LeftoversEnoughForDinner { get; set; }

    /// <inheritdoc />
    public bool IsLeftovers { get; set; }

    /// <inheritdoc />
    public int DishId { get; set; }

    #endregion

    #region Implementation of IEntity

    /// <inheritdoc />
    public byte[] Version { get; set; }

    /// <inheritdoc />
    public DateTime CreatedDateTime { get; set; }

    /// <inheritdoc />
    public DateTime UpdatedDateTime { get; set; }

    #endregion

    #region Implementation of IDinner

    /// <inheritdoc />
    public IDish Dish { get; set; }

    #endregion

    #region Constructors

    public Dinner()
    {
    }

    [JsonConstructor]
    private Dinner(int id)
    {
        this.id = id;
    }

    #endregion
}
