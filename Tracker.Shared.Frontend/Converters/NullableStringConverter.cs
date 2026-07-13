using Microsoft.UI.Xaml.Data;

namespace Tracker.Shared.Frontend.Converters;

internal sealed class NullableStringConverter(string nullText) : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string text && !string.IsNullOrWhiteSpace(text))
            return text;

        return nullText;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}
