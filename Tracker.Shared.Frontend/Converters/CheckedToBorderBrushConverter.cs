using Microsoft.UI;
using Microsoft.UI.Xaml.Data;

namespace Tracker.Shared.Frontend.Converters;

internal sealed class CheckedToBorderBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value is true ? new SolidColorBrush(Colors.DodgerBlue) : new SolidColorBrush(Colors.Black);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}
