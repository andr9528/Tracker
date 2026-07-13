using Tracker.Module.Budget.Abstraction.Enums;
using Tracker.Module.Budget.Abstraction.Searchable;

namespace Tracker.Module.Budget.Model.Searchable;

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
