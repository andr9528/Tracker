using Tracker.Shared.Abstraction.Enums.Budget;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Searchable
{
    public interface ISearchableCommonPayment
    {
        double Amount { get; set; }
        CurrencyCodes Currency { get; set; }
        int PaymentTypeId { get; set; }
    }
}