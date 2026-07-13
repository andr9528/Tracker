using Tracker.Module.Budget.Abstraction.Entity;

namespace Tracker.Module.Budget.Model.Entity;

public class RecurringPayment : IRecurringPayment
{
    private readonly int id;

    /// <inheritdoc />
    public int Id
    {
        get => id;
        set => throw new InvalidOperationException($"{nameof(Id)} cannot be changed");
    }

    /// <inheritdoc />
    public DateOnly Start { get; set; }

    /// <inheritdoc />
    public DateOnly? End { get; set; }

    /// <inheritdoc />
    public int PaymentTemplateId { get; set; }

    /// <inheritdoc />
    public byte[] Version { get; set; }

    /// <inheritdoc />
    public DateTime CreatedDateTime { get; set; }

    /// <inheritdoc />
    public DateTime UpdatedDateTime { get; set; }

    /// <inheritdoc />
    public IPaymentTemplate PaymentTemplate { get; set; }

    /// <inheritdoc />
    public ICollection<IPayment> Payments { get; set; }

    public RecurringPayment(int id)
    {
        this.id = id;
    }
}
