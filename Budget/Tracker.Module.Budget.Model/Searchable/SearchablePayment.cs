using Tracker.Module.Budget.Abstraction.Enums;
using Tracker.Module.Budget.Abstraction.Searchable;

namespace Tracker.Module.Budget.Model.Searchable;

public class SearchablePayment : ISearchablePayment
{
    #region Implementation of ISearchable

    /// <inheritdoc />
    public int Id { get; set; }

    #endregion

    #region Implementation of ISearchablePayment

    /// <inheritdoc />
    public DateOnly Date { get; set; }

    /// <inheritdoc />
    public int? RecurringPaymentId { get; set; }

    #endregion

    #region Implementation of ISearchableCommonPayment

    /// <inheritdoc />
    public double Amount { get; set; }

    /// <inheritdoc />
    public CurrencyCodes Currency { get; set; }

    /// <inheritdoc />
    public int PaymentTypeId { get; set; }

    #endregion
}
