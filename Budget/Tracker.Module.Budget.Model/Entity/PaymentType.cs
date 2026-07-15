using Newtonsoft.Json;
using Tracker.Module.Budget.Abstraction.Entity;

namespace Tracker.Module.Budget.Model.Entity;

public class PaymentType : IPaymentType
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

    #region Implementation of ISearchablePaymentType

    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public string Description { get; set; }

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

    public PaymentType()
    {
    }

    [JsonConstructor]
    private PaymentType(int id)
    {
        this.id = id;
    }

    #endregion
}
