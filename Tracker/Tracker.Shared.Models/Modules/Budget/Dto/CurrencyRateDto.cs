using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Budget.Dto;

namespace Tracker.Shared.Models.Modules.Budget.Dto
{
    public class CurrencyRateDto : ICurrencyRateDto
    {
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