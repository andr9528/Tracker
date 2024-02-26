using Tracker.Shared.Abstraction.Interfaces.Budget.Dto;

namespace Tracker.Shared.Models.Modules.Budget.Dto
{
    public class RecurringPaymentDto : IRecurringPaymentDto
    {
        /// <inheritdoc />
        public DateOnly Start { get; set; }

        /// <inheritdoc />
        public DateOnly? End { get; set; }

        /// <inheritdoc />
        public int PaymentTemplateId { get; set; }

        /// <inheritdoc />
        public int CoreUserId { get; set; }
    }
}