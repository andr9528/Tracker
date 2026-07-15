using Tracker.Module.Budget.Abstraction.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Budget.Abstraction.Entity;

public interface IRecurringPayment : ISearchableRecurringPayment, IEntity
{
    IPaymentTemplate PaymentTemplate { get; set; }
    ICollection<IPayment> Payments { get; set; }
}
