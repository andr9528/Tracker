using Microsoft.UI.Xaml.Data;

namespace Tracker.Shared.Frontend.Converters;

public sealed class TwoDecimalConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value switch
        {
            float number => number.ToString("0.##"),
            double number => number.ToString("0.##"),
            decimal number => number.ToString("0.##"),
            var _ => string.Empty,
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}
