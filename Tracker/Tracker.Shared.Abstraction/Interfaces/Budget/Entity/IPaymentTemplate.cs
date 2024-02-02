using Tracker.Shared.Abstraction.Interfaces.Budget.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Entity
{
    public interface IPaymentTemplate : ICommonPayment, ISearchablePaymentTemplate, IEntity
    {
        IRecurringPayment RecurringPayment { get; set; }
    }
}