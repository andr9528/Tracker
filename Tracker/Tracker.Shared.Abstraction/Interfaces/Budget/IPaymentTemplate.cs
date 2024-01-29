using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget
{
    public interface IPaymentTemplate : ISearchablePaymentTemplate, IEntity
    {
        IRecurringPayment RecurringPayment { get; set; }
    }
}