using Tracker.Shared.Abstraction.Enums.Budget;

namespace Tracker.Shared.Abstraction.Interfaces.Budget
{
    public interface ISearchableCommonPayment
    {
        double Amount { get; set; }
        CurrencyCodes Currency { get; set; }
    }
}