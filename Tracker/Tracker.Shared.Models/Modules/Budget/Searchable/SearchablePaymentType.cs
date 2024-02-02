using Tracker.Shared.Abstraction.Interfaces.Budget.Searchable;

namespace Tracker.Shared.Models.Modules.Budget.Searchable
{
    public class SearchablePaymentType : ISearchablePaymentType
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }
    }
}