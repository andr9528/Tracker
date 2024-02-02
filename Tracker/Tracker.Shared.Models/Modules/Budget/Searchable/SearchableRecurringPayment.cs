using Tracker.Shared.Abstraction.Interfaces.Budget.Searchable;

namespace Tracker.Shared.Models.Modules.Budget
{
    public class SearchableRecurringPayment : ISearchableRecurringPayment
    {
        /// <inheritdoc />
        public int Id { get; set; }

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