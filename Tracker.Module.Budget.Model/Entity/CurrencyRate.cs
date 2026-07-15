using Newtonsoft.Json;
using Tracker.Module.Budget.Abstraction.Entity;
using Tracker.Module.Budget.Abstraction.Enums;

namespace Tracker.Module.Budget.Model.Entity;

public class CurrencyRate : ICurrencyRate
{
    #region Fields

    private readonly int id;

    #endregion

    #region Implementation of ISearchable

    /// <inheritdoc />
    public int Id
    {
        get => id;
        set => throw new InvalidOperationException($"{nameof(Id)} cannot be changed");
    }

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

    #region Implementation of IEntity

    /// <inheritdoc />
    public byte[] Version { get; set; }

    /// <inheritdoc />
    public DateTime CreatedDateTime { get; set; }

    /// <inheritdoc />
    public DateTime UpdatedDateTime { get; set; }

    #endregion

    #region Constructors

    public CurrencyRate()
    {
    }

    [JsonConstructor]
    private CurrencyRate(int id)
    {
        this.id = id;
    }

    #endregion
}
