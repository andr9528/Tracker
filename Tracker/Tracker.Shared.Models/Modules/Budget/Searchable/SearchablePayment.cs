using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Budget.Searchable;

namespace Tracker.Shared.Models.Modules.Budget.Searchable
{
    public class SearchablePayment : ISearchablePayment
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public DateOnly Date { get; set; }

        /// <inheritdoc />
        public int? RecurringPaymentId { get; set; }

        /// <inheritdoc />
        public int CoreUserId { get; set; }

        /// <inheritdoc />
        public double Amount { get; set; }

        /// <inheritdoc />
        public CurrencyCodes Currency { get; set; }

        /// <inheritdoc />
        public int PaymentTypeId { get; set; }
    }
}