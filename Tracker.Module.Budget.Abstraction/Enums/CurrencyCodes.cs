using System.ComponentModel;

namespace Tracker.Module.Budget.Abstraction.Enums;

public enum CurrencyCodes
{
    [Description("NULL - Default Value")] NULL = 0,
    [Description("DKK - Danish krone")] DKK = 208,
    [Description("EUR - Euro")] EUR = 978,
    [Description("GBP - Pound sterling")] GBP = 826,
    [Description("USD - United States dollar")] USD = 840,
}
