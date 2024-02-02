using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Budget.Searchable;

namespace Tracker.Shared.Models.Modules.Budget.Searchable
{
    public class SearchableCurrencyRate : ISearchableCurrencyRate
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public CurrencyCodes FromCurrency { get; set; }

        /// <inheritdoc />
        public CurrencyCodes ToCurrency { get; set; }

        /// <inheritdoc />
        public double Rate { get; set; }

        /// <inheritdoc />
        public DateOnly Date { get; set; }
    }
}