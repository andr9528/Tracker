namespace Tracker.Shared.Frontend.Factory;

public static class StackPanelFactory
{
    public static StackPanel CreateDefaultPanel()
    {
        return new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(5),
        };
    }
}