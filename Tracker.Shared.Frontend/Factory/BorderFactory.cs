namespace Tracker.Shared.Frontend.Factory;

public static class BorderFactory
{
    public static void ConfigurePageBorder(this Border border)
    {
        border.Margin = new Thickness(0);
        border.Padding = new Thickness(2);
    }

    public static void ConfigurePieceBorder(this Border border)
    {
        border.Margin = new Thickness(0);
        border.Padding = new Thickness(0);
    }
}