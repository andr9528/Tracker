using Tracker.Module.Budget.Abstraction.Enums;
using Tracker.Module.Budget.Abstraction.Searchable;

namespace Tracker.Module.Budget.Model.Searchable;

public class SearchablePayment : ISearchablePayment
{
    /// <inheritdoc />
    public int Id { get; set; }

    /// <inheritdoc />
    public DateOnly Date { get; set; }

    /// <inheritdoc />
    public int? RecurringPaymentId { get; set; }

    /// <inheritdoc />
    public double Amount { get; set; }

    /// <inheritdoc />
    public CurrencyCodes Currency { get; set; }

    /// <inheritdoc />
    public int PaymentTypeId { get; set; }
}
