using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Budget.Dto;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Searchable
{
    public interface ISearchableCurrencyRate : ICurrencyRateDto, ISearchable
    {
    }
}