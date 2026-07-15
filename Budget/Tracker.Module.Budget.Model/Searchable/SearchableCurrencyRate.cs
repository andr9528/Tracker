using Tracker.Module.Budget.Abstraction.Enums;
using Tracker.Module.Budget.Abstraction.Searchable;

namespace Tracker.Module.Budget.Model.Searchable;

public class SearchableCurrencyRate : ISearchableCurrencyRate
{
    #region Implementation of ISearchable

    /// <inheritdoc />
    public int Id { get; set; }

    #endregion

    #region Implementation of ISearchableCurrencyRate

    /// <inheritdoc />
    public CurrencyCodes FromCurrency { get; set; }

    /// <inheritdoc />
    public CurrencyCodes ToCurrency { get; set; }

    /// <inheritdoc />
    public double Rate { get; set; }

    /// <inheritdoc />
    public DateOnly Date { get; set; }

    #endregion
}
