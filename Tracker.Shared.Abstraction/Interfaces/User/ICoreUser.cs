using Tracker.Shared.Abstraction.Interfaces.Budget.Entity;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.User
{
    public interface ICoreUser : ISearchableCoreUser, IEntity
    {
        ICollection<IPayment> Payments { get; set; }
        ICollection<IRecurringPayment> RecurringPayments { get; set; }
    }
}