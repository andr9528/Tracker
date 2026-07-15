using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Tracker.Shared.Frontend.Extensions;

namespace Tracker.Shared.Frontend.Factory;

public static class CheckBoxFactory
{
    public static CheckBox CreateLightCheckBox(string bindingPath)
    {
        CheckBox checkBox = new()
        {
            Foreground = new SolidColorBrush(Colors.Black),
            Margin = new Thickness(4),
            MinHeight = 32,
            Template = CreateLightCheckBoxTemplate(),
        };

        checkBox.SetBinding(ToggleButton.IsCheckedProperty, new Binding
        {
            Path = new PropertyPath(bindingPath),
            Mode = BindingMode.TwoWay,
        });

        return checkBox;
    }

    public static Grid CreateLightCheckBoxWithLabel(string label, string bindingPath, out CheckBox checkBox)
    {
        Grid grid = GridFactory.CreateDefaultGrid();

        grid.ColumnSpacing = 4;
        grid.DefineColumns(GridUnitType.Auto, [1,]);
        grid.DefineColumns(GridUnitType.Star, [1,]);

        checkBox = CreateLightCheckBox(bindingPath).SetColumn(0);

        TextBlock textBlock = TextBlockFactory.CreateBlackText(label).SetColumn(1);

        grid.Children.Add(checkBox);
        grid.Children.Add(textBlock);

        return grid;
    }

    private static ControlTemplate CreateLightCheckBoxTemplate()
    {
        return new ControlTemplate(() =>
        {
            Border box = CreateCheckBoxBorder();

            TextBlock checkMark = CreateCheckMark();
            box.Child = checkMark;

            return box;
        });
    }

    private static Border CreateCheckBoxBorder()
    {
        Border border = new()
        {
            Width = 24,
            Height = 24,
            Background = new SolidColorBrush(Colors.White),
            BorderBrush = new SolidColorBrush(Colors.Black),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(4),
        };

        border.SetBinding(FrameworkElement.BackgroundProperty, CreateCheckedBackgroundBinding());
        border.SetBinding(Border.BorderBrushProperty, CreateCheckedBorderBrushBinding());

        return border;
    }

    private static TextBlock CreateCheckMark()
    {
        TextBlock checkMark = TextBlockFactory.CreateBlackText();

        checkMark.Text = "✓";
        checkMark.FontSize = 18;
        checkMark.FontWeight = FontWeights.Bold;
        checkMark.HorizontalAlignment = HorizontalAlignment.Center;
        checkMark.VerticalAlignment = VerticalAlignment.Center;

        checkMark.SetBinding(UIElement.VisibilityProperty, CreateCheckedVisibilityBinding());

        return checkMark;
    }

    private static Binding CreateCheckedBackgroundBinding()
    {
        Binding binding = CreateTemplateBinding(nameof(CheckBox.IsChecked));
        binding.Converter = new CheckedToBackgroundBrushConverter();

        return binding;
    }

    private static Binding CreateCheckedBorderBrushBinding()
    {
        Binding binding = CreateTemplateBinding(nameof(CheckBox.IsChecked));
        binding.Converter = new CheckedToBorderBrushConverter();

        return binding;
    }

    private static Binding CreateCheckedVisibilityBinding()
    {
        Binding binding = CreateTemplateBinding(nameof(CheckBox.IsChecked));
        binding.Converter = new CheckedToVisibilityConverter();

        return binding;
    }

    private static Binding CreateTemplateBinding(string propertyName)
    {
        return new Binding
        {
            RelativeSource = new RelativeSource
            {
                Mode = RelativeSourceMode.TemplatedParent,
            },
            Path = new PropertyPath(propertyName),
        };
    }

    private sealed class CheckedToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is true ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }

    private sealed class CheckedToBackgroundBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is true ? new SolidColorBrush(Colors.DodgerBlue) : new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }

    private sealed class CheckedToBorderBrushConverter : IValueConverter
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
}
