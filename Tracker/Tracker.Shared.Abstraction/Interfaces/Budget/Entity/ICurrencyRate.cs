using Tracker.Shared.Abstraction.Interfaces.Budget.Dto;
using Tracker.Shared.Abstraction.Interfaces.Budget.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Entity
{
    public interface ICurrencyRate : ISearchableCurrencyRate, IEntity
    {
    }
}