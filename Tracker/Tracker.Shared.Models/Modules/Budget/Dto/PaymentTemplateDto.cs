using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Budget.Dto;

namespace Tracker.Shared.Models.Modules.Budget.Dto
{
    public class PaymentTemplateDto : IPaymentTemplateDto
    {
        /// <inheritdoc />
        public double Amount { get; set; }

        /// <inheritdoc />
        public CurrencyCodes Currency { get; set; }

        /// <inheritdoc />
        public int PaymentTypeId { get; set; }

        /// <inheritdoc />
        public int RecurringPaymentId { get; set; }
    }
}