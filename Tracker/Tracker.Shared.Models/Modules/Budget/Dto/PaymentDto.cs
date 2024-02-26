using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Budget.Dto;

namespace Tracker.Shared.Models.Modules.Budget.Dto
{
    public class PaymentDto : IPaymentDto

    {
        /// <inheritdoc />
        public double Amount { get; set; }

        /// <inheritdoc />
        public CurrencyCodes Currency { get; set; }

        /// <inheritdoc />
        public int PaymentTypeId { get; set; }

        /// <inheritdoc />
        public DateOnly Date { get; set; }

        /// <inheritdoc />
        public int? RecurringPaymentId { get; set; }

        /// <inheritdoc />
        public int CoreUserId { get; set; }
    }
}