using Tracker.Shared.Abstraction.Interfaces.Budget.Dto;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Searchable
{
    public interface ISearchableRecurringPayment : IRecurringPaymentDto, ISearchable
    {
    }
}