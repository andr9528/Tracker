using Newtonsoft.Json;
using Tracker.Module.Budget.Abstraction.Entity;

namespace Tracker.Module.Budget.Model.Entity;

public class RecurringPayment : IRecurringPayment
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

    #region Implementation of ISearchableRecurringPayment

    /// <inheritdoc />
    public DateOnly Start { get; set; }

    /// <inheritdoc />
    public DateOnly? End { get; set; }

    /// <inheritdoc />
    public int PaymentTemplateId { get; set; }

    #endregion

    #region Implementation of IEntity

    /// <inheritdoc />
    public byte[] Version { get; set; }

    /// <inheritdoc />
    public DateTime CreatedDateTime { get; set; }

    /// <inheritdoc />
    public DateTime UpdatedDateTime { get; set; }

    #endregion

    #region Implementation of IRecurringPayment

    /// <inheritdoc />
    public IPaymentTemplate PaymentTemplate { get; set; }

    /// <inheritdoc />
    public ICollection<IPayment> Payments { get; set; }

    #endregion

    #region Constructors

    [JsonConstructor]
    public RecurringPayment(int id)
    {
        this.id = id;
    }

    #endregion
}
