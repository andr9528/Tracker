using Tracker.Module.Budget.Abstraction.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Budget.Abstraction.Entity;

public interface IPaymentTemplate : ICommonPayment, ISearchablePaymentTemplate, IEntity
{
    IRecurringPayment RecurringPayment { get; set; }
}
