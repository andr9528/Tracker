using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Budget.Abstraction.Searchable;

public interface ISearchablePayment : ISearchableCommonPayment, ISearchable
{
    DateOnly Date { get; set; }
    int? RecurringPaymentId { get; set; }
}
