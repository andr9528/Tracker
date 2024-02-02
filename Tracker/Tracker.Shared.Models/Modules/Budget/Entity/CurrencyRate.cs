using Tracker.Shared.Abstraction.Enums.Budget;
using Tracker.Shared.Abstraction.Interfaces.Budget.Entity;

namespace Tracker.Shared.Models.Modules.Budget.Entity
{
    public class CurrencyRate : ICurrencyRate
    {
        private readonly int id;

        /// <inheritdoc />
        public int Id
        {
            get => id;
            set => throw new InvalidOperationException($"{nameof(Id)} cannot be changed");
        }

        /// <inheritdoc />
        public CurrencyCodes FromCurrency { get; set; }

        /// <inheritdoc />
        public CurrencyCodes ToCurrency { get; set; }

        /// <inheritdoc />
        public double Rate { get; set; }

        /// <inheritdoc />
        public DateOnly Date { get; set; }

        /// <inheritdoc />
        public byte[] Version { get; set; }

        /// <inheritdoc />
        public DateTime CreatedDateTime { get; set; }

        /// <inheritdoc />
        public DateTime UpdatedDateTime { get; set; }

        public CurrencyRate()
        {
        }

        private CurrencyRate(int id)
        {
            this.id = id;
        }
    }
}