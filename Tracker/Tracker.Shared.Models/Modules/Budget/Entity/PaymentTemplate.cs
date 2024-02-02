using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Budget.Entity;

namespace Tracker.Shared.Models.Modules.Budget.Entity
{
    public class PaymentTemplate : IPaymentTemplate
    {
        private readonly int id;

        /// <inheritdoc />
        public int Id
        {
            get => id;
            set => throw new InvalidOperationException($"{nameof(Id)} cannot be changed");
        }

        /// <inheritdoc />
        public double Amount { get; set; }

        /// <inheritdoc />
        public CurrencyCodes Currency { get; set; }

        /// <inheritdoc />
        public int PaymentTypeId { get; set; }

        /// <inheritdoc />
        public int RecurringPaymentId { get; set; }

        /// <inheritdoc />
        public IRecurringPayment RecurringPayment { get; set; }

        /// <inheritdoc />
        public byte[] Version { get; set; }

        /// <inheritdoc />
        public DateTime CreatedDateTime { get; set; }

        /// <inheritdoc />
        public DateTime UpdatedDateTime { get; set; }

        /// <inheritdoc />
        public IPaymentType PaymentType { get; set; }

        public PaymentTemplate()
        {
        }

        private PaymentTemplate(int id)
        {
            this.id = id;
        }
    }
}