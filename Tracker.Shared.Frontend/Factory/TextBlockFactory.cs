using Microsoft.UI;

namespace Tracker.Shared.Frontend.Factory;

public static class TextBlockFactory
{
    public static TextBlock CreateHeader(string text)
    {
        return new TextBlock
        {
            Text = text,
            FontSize = 24,
            Margin = new Thickness(0, 0, 0, 10),
            HorizontalAlignment = HorizontalAlignment.Center,
            Foreground = new SolidColorBrush(Colors.Black),
        };
    }

    public static TextBlock CreateBlackText(string text = "")
    {
        return new TextBlock
        {
            Text = text,
            Foreground = new SolidColorBrush(Colors.Black),
            VerticalAlignment = VerticalAlignment.Center,
        };
    }
}
