using Tracker.Module.Budget.Abstraction.Enums;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Budget.Abstraction.Searchable;

public interface ISearchableCurrencyRate : ISearchable
{
    CurrencyCodes FromCurrency { get; set; }
    CurrencyCodes ToCurrency { get; set; }
    double Rate { get; set; }
    DateOnly Date { get; set; }
}
