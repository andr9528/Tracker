﻿using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Budget;
using Tracker.Shared.Abstraction.Interfaces.User;

namespace Tracker.Shared.Models.Modules.Budget
{
    public class Payment : IPayment
    {
        private readonly int id;

        /// <inheritdoc />
        public int Id
        {
            get => id;
            set => throw new InvalidOperationException($"{nameof(Id)} cannot be changed");
        }

        /// <inheritdoc />
        public DateOnly Date { get; set; }

        /// <inheritdoc />
        public int? RecurringPaymentId { get; set; }

        /// <inheritdoc />
        public int UserId { get; set; }

        /// <inheritdoc />
        public IRecurringPayment? RecurringPayment { get; set; }

        /// <inheritdoc />
        public ICoreUser CoreUser { get; set; }

        /// <inheritdoc />
        public double Amount { get; set; }

        /// <inheritdoc />
        public CurrencyCodes Currency { get; set; }

        /// <inheritdoc />
        public byte[] Version { get; set; }

        /// <inheritdoc />
        public DateTime CreatedDateTime { get; set; }

        /// <inheritdoc />
        public DateTime UpdatedDateTime { get; set; }

        public Payment()
        {

        }

        private Payment(int id)
        {
            this.id = id;
        }

    }
}