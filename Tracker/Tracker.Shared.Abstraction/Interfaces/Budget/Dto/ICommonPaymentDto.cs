using Tracker.Shared.Abstraction.Enums.Budget;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Dto
{
    public interface ICommonPaymentDto
    {
        double Amount { get; set; }
        CurrencyCodes Currency { get; set; }
        int PaymentTypeId { get; set; }
    }
}