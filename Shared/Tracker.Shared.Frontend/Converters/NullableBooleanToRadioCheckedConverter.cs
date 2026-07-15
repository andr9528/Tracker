using Microsoft.UI.Xaml.Data;

namespace Tracker.Shared.Frontend.Converters;

internal sealed class NullableBooleanToRadioCheckedConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var currentValue = value as bool?;
        var targetValue = parameter as bool?;

        return currentValue == targetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return value is true ? parameter : DependencyProperty.UnsetValue;
    }
}
