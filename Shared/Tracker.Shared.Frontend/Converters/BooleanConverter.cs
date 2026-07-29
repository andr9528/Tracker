using Microsoft.UI.Xaml.Data;

namespace Tracker.Shared.Frontend.Converters;

public sealed class BooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value switch
        {
            bool boolean => boolean ? "Yes" : "No",
            var _ => value,
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}
