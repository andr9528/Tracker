using Tracker.Shared.Abstraction.Interfaces.Budget.Dto;

namespace Tracker.Shared.Models.Modules.Budget.Dto
{
    public class PaymentTypeDto : IPaymentTypeDto
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }
    }
}