using Tracker.Module.Budget.Abstraction.Searchable;

namespace Tracker.Module.Budget.Model.Searchable;

public class SearchableRecurringPayment : ISearchableRecurringPayment
{
    #region Implementation of ISearchable

    /// <inheritdoc />
    public int Id { get; set; }

    #endregion

    #region Implementation of ISearchableRecurringPayment

    /// <inheritdoc />
    public DateOnly Start { get; set; }

    /// <inheritdoc />
    public DateOnly? End { get; set; }

    /// <inheritdoc />
    public int PaymentTemplateId { get; set; }

    #endregion
}
