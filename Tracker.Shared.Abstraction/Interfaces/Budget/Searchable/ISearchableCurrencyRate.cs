using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Searchable
{
    public interface ISearchableCurrencyRate : ISearchable
    {
        CurrencyCodes FromCurrency { get; set; }
        CurrencyCodes ToCurrency { get; set; }
        double Rate { get; set; }
        DateOnly Date { get; set; }
    }
}