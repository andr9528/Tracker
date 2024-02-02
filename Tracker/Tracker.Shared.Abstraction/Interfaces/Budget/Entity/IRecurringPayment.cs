using Tracker.Shared.Abstraction.Interfaces.Budget.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Abstraction.Interfaces.User;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Entity
{
    public interface IRecurringPayment : ISearchableRecurringPayment, IEntity
    {
        IPaymentTemplate PaymentTemplate { get; set; }
        ICollection<IPayment> Payments { get; set; }
        ICoreUser CoreUser { get; set; }
    }
}