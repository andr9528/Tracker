using System.ComponentModel;

namespace Tracker.Shared.Abstraction.Enums.Budget
{
    public enum CurrencyCodes
    {
        [Description("DKK - Danish krone")] DKK = 208,
        [Description("EUR - Euro")] EUR = 978,
        [Description("GBP - Pound sterling")] GBP = 826,
        [Description("USD - United States dollar")] USD = 840,
    }
}