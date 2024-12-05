using System.Globalization;
using System.Text;
using Tracker.Module.Budget.Presentation;
using Tracker.Module.Dining.Presentation;
using Tracker.Module.Time.Presentation;

namespace Tracker.Frontend.Uno.Models;

public class TrackerModule
{
    public enum Module
    {
        BUDGET = 0,
        DINING = 1,
        TIME = 2,
    }

    private readonly Module typeModule;

    public Module TypeModule => typeModule;

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
                return new TimeTabs();
            case Module.DINING:
                return new DiningTabs();
            case Module.BUDGET:
            default:
                return new BudgetTabs();
        }
    }
}
