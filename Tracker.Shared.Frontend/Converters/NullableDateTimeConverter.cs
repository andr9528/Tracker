using Microsoft.UI.Xaml.Data;

namespace Tracker.Shared.Frontend.Converters;

internal sealed class NullableDateTimeConverter(string nullText) : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value switch
        {
            null => nullText,
            DateTime dateTime => dateTime.ToString("dd.MM.yyyy HH:mm"),
            DateTimeOffset dateTimeOffset => dateTimeOffset.ToString("dd.MM.yyyy HH:mm"),
            var _ => value,
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}
