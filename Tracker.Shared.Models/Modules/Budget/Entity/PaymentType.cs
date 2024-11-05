using Tracker.Shared.Abstraction.Interfaces.Budget.Entity;

namespace Tracker.Shared.Models.Modules.Budget.Entity
{
    public class PaymentType : IPaymentType
    {
        private readonly int id;

        /// <inheritdoc />
        public int Id
        {
            get => id;
            set => throw new InvalidOperationException($"{nameof(Id)} cannot be changed");
        }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public byte[] Version { get; set; }

        /// <inheritdoc />
        public DateTime CreatedDateTime { get; set; }

        /// <inheritdoc />
        public DateTime UpdatedDateTime { get; set; }

        public PaymentType()
        {
        }

        private PaymentType(int id)
        {
            this.id = id;
        }
    }
}