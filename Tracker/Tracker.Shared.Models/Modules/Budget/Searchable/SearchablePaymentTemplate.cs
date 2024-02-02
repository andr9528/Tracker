using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Budget.Searchable;

namespace Tracker.Shared.Models.Modules.Budget.Searchable
{
    public class SearchablePaymentTemplate : ISearchablePaymentTemplate
    {
        /// <inheritdoc />
        public int Id { get; set; }

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