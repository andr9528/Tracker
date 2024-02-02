using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Abstraction.Interfaces.Budget.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Abstraction.Interfaces.User;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Entity
{
    public interface IPayment : ICommonPayment, ISearchablePayment, IEntity
    {
        IRecurringPayment? RecurringPayment { get; set; }
        ICoreUser CoreUser { get; set; }
    }
}