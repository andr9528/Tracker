using Microsoft.UI.Xaml.Data;

namespace Tracker.Shared.Frontend.Converters;

public sealed class InverseBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value is bool boolean && !boolean;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return value is bool boolean && !boolean;
    }
}
