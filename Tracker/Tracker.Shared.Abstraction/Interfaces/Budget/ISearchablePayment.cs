using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget
{
    public interface ISearchablePayment : ISearchableCommonPayment, ISearchable
    {
        DateOnly Date { get; set; }
        int? RecurringPaymentId { get; set; }
        int CoreUserId { get; set; }
    }
}