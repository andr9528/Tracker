using System.Globalization;
using System.Text;
using Tracker.Module.Budget.Presentation;

namespace Tracker.Frontend.Uno.Models;

public class TrackerModule
{
    public enum Module
    {
        TIME,
        DINING,
        BUDGET,
    }

    private readonly Module typeModule;

    public TrackerModule(Module typeModule)
    {
        this.typeModule = typeModule;
    }

    public static string GetModuleAsReadableString(Module typeModule)
    {
        var moduleAsString = typeModule.ToString();
        var builder = new StringBuilder();

        foreach (char c in moduleAsString)
        {
            builder.Append(!char.IsLetterOrDigit(c) ? " " : c);
        }

        var builtModule = builder.ToString();
        builtModule = builtModule.ToLower();

        TextInfo textInfo = new CultureInfo("en-GB", false).TextInfo;

        return textInfo.ToTitleCase(builtModule);
    }

    public string GetModuleAsReadableString()
    {
        return GetModuleAsReadableString(typeModule);
    }

    public UserControl GetModuleControl()
    {
        switch (typeModule)
        {
            case Module.TIME:
            case Module.DINING:
            case Module.BUDGET:
            default:
                return new BudgetTabs();
        }
    }
}
