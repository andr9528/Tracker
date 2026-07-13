using Tracker.Module.Budget.Abstraction.Enums;

namespace Tracker.Module.Budget.Abstraction.Searchable;

public interface ISearchableCommonPayment
{
    double Amount { get; set; }
    CurrencyCodes Currency { get; set; }
    int PaymentTypeId { get; set; }
}
