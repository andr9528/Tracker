using Tracker.Module.Budget.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Budget.Model.ComplexSearchable;

public class ComplexSearchableRecurringPayment : IComplexSearchable<SearchableRecurringPayment>
{
    public DateOnly? StartFrom { get; set; }

    public DateOnly? StartTo { get; set; }

    public DateOnly? EndFrom { get; set; }

    public DateOnly? EndTo { get; set; }

    /// <inheritdoc />
    public SearchableRecurringPayment Searchable { get; set; } = new();
}
