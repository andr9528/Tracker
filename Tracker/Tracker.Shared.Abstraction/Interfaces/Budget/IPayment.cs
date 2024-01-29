using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Abstraction.Interfaces.User;

namespace Tracker.Shared.Abstraction.Interfaces.Budget
{
    public interface IPayment : ISearchableCommonPayment, IEntity
    {
        IRecurringPayment? RecurringPayment { get; set; }
        ICoreUser CoreUser { get; set; }
    }
}