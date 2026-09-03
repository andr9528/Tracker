using Newtonsoft.Json;
using Tracker.Module.Dining.Abstraction.Entity;

namespace Tracker.Module.Dining.Model.Entity;

public class DinnerTemplate : IDinnerTemplate
{
    #region Fields

    private readonly int id;

    #endregion

    #region Implementation of ICommonDinnerTemplate

    /// <inheritdoc />
    public bool IsTakeAway { get; set; }

    /// <inheritdoc />
    public bool HasLeftovers { get; set; }

    /// <inheritdoc />
    public bool LeftoversEnoughForDinner { get; set; }

    /// <inheritdoc />
    public bool IsLeftovers { get; set; }

    /// <inheritdoc />
    public bool IsEatenOut { get; set; }

    /// <inheritdoc />
    public bool IsReadyMadeDish { get; set; }

    #endregion

    #region Implementation of ISearchable

    /// <inheritdoc />
    public int Id
    {
        get => id;
        set => throw new InvalidOperationException($"{nameof(Id)} cannot be changed");
    }

    #endregion

    #region Implementation of ISearchableDinnerTemplate

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

    #region Constructors

    public DinnerTemplate()
    {
    }

    [JsonConstructor]
    private DinnerTemplate(int id)
    {
        this.id = id;
    }

    #endregion
}
