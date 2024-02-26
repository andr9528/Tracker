using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Dto
{
    public interface IPaymentDto : ICommonPaymentDto, IDto
    {
        DateOnly Date { get; set; }
        int? RecurringPaymentId { get; set; }
        int CoreUserId { get; set; }
    }
}