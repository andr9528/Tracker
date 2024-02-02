using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Searchable
{
    public interface ISearchablePaymentTemplate : ISearchableCommonPayment, ISearchable
    {
        int RecurringPaymentId { get; set; }

    }
}