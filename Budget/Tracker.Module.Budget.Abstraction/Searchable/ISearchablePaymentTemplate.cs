using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Budget.Abstraction.Searchable;

public interface ISearchablePaymentTemplate : ISearchableCommonPayment, ISearchable
{
    int RecurringPaymentId { get; set; }
}
