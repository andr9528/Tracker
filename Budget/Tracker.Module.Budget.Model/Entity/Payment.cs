using Newtonsoft.Json;
using Tracker.Module.Budget.Abstraction.Entity;
using Tracker.Module.Budget.Abstraction.Enums;

namespace Tracker.Module.Budget.Model.Entity;

public class Payment : IPayment
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

    #region Implementation of ISearchablePayment

    /// <inheritdoc />
    public DateOnly Date { get; set; }

    /// <inheritdoc />
    public int? RecurringPaymentId { get; set; }

    #endregion

    #region Implementation of IPayment

    /// <inheritdoc />
    public IRecurringPayment? RecurringPayment { get; set; }

    #endregion

    #region Implementation of ISearchableCommonPayment

    /// <inheritdoc />
    public double Amount { get; set; }

    /// <inheritdoc />
    public CurrencyCodes Currency { get; set; }

    /// <inheritdoc />
    public int PaymentTypeId { get; set; }

    #endregion

    #region Implementation of IEntity

    /// <inheritdoc />
    public byte[] Version { get; set; }

    /// <inheritdoc />
    public DateTime CreatedDateTime { get; set; }

    /// <inheritdoc />
    public DateTime UpdatedDateTime { get; set; }

    #endregion

    #region Implementation of ICommonPayment

    /// <inheritdoc />
    public IPaymentType PaymentType { get; set; }

    #endregion

    #region Constructors

    public Payment()
    {
    }

    [JsonConstructor]
    private Payment(int id)
    {
        this.id = id;
    }

    #endregion
}
