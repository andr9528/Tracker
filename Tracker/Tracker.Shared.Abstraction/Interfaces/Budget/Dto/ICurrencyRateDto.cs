using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Dto
{
    public interface ICurrencyRateDto : IDto
    {
        CurrencyCodes FromCurrency { get; set; }
        CurrencyCodes ToCurrency { get; set; }
        double Rate { get; set; }
        DateOnly Date { get; set; }
    }
}