using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Dto
{
    public interface IPaymentTemplateDto : ICommonPaymentDto, IDto
    {
        int RecurringPaymentId { get; set; }
    }
}