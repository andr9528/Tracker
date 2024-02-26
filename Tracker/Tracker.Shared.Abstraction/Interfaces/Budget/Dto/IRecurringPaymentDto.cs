using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Dto
{
    public interface IRecurringPaymentDto : IDto
    {
        DateOnly Start { get; set; }
        DateOnly? End { get; set; }
        int PaymentTemplateId { get; set; }
        int CoreUserId { get; set; }
    }
}