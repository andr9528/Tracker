using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Tracker.Shared.Frontend.Extensions;

namespace Tracker.Shared.Frontend.Factory;

public static class TextBoxFactory
{
    public static TextBox CreateSearchBox(string header, string placeholderText, string bindingPath)
    {
        var textBox = new TextBox
        {
            Header = header,
            PlaceholderText = placeholderText,
            Foreground = new SolidColorBrush(Colors.Black),
            PlaceholderForeground = new SolidColorBrush(Colors.Gray),
            Background = new SolidColorBrush(Colors.White),
            BorderBrush = new SolidColorBrush(Colors.Black),
            Margin = new Thickness(4),
            MinHeight = 56,
            Template = CreateLightTextBoxTemplate(),
            VerticalAlignment = VerticalAlignment.Center,
        };

        textBox.SetBinding(TextBox.TextProperty, new Binding
        {
            Path = new PropertyPath(bindingPath),
            Mode = BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
        });

        return textBox;
    }

    public static TextBox CreateMultilineTextBox(string header, string placeholderText, string bindingPath)
    {
        TextBox textBox = CreateSearchBox(header, placeholderText, bindingPath);

        textBox.AcceptsReturn = true;
        textBox.TextWrapping = TextWrapping.Wrap;
        textBox.MinHeight = 120;

        return textBox;
    }

    private static ControlTemplate CreateLightTextBoxTemplate()
    {
        return new ControlTemplate(() =>
        {
            Grid grid = CreateTemplateGrid();
            TextBlock header = CreateHeaderTextBlock();
            Border border = CreateInputBorder();

            border.Child = CreateInputGrid();

            grid.Children.Add(header);
            grid.Children.Add(border.SetRow(1));

            return grid;
        });
    }

    private static Grid CreateTemplateGrid()
    {
        Grid grid = GridFactory.CreateDefaultGrid();

        grid.DefineRows(GridLength.Auto, new GridLength(1, GridUnitType.Star));

        return grid;
    }

    private static TextBlock CreateHeaderTextBlock()
    {
        TextBlock header = TextBlockFactory.CreateBlackText().SetRow(0);

        header.Margin = new Thickness(6, 4, 8, 0);

        header.SetBinding(TextBlock.TextProperty, CreateTemplateBinding(nameof(TextBox.Header)));

        return header;
    }

    private static Grid CreateInputGrid()
    {
        Grid inputGrid = GridFactory.CreateDefaultGrid();

        inputGrid.Children.Add(CreatePlaceholderTextBlock());
        inputGrid.Children.Add(CreateContentElement());

        return inputGrid;
    }

    private static TextBlock CreatePlaceholderTextBlock()
    {
        TextBlock placeholder = TextBlockFactory.CreateBlackText();

        placeholder.Foreground = new SolidColorBrush(Colors.Gray);
        placeholder.VerticalAlignment = VerticalAlignment.Center;
        placeholder.IsHitTestVisible = false;

        placeholder.SetBinding(TextBlock.TextProperty, CreateTemplateBinding(nameof(TextBox.PlaceholderText)));
        placeholder.SetBinding(UIElement.VisibilityProperty, CreatePlaceholderVisibilityBinding());

        return placeholder;
    }

    private static ScrollViewer CreateContentElement()
    {
        return new ScrollViewer
        {
            Name = "ContentElement",
            HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
            VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
            IsTabStop = false,
            VerticalAlignment = VerticalAlignment.Center,
        };
    }

    private static Border CreateInputBorder()
    {
        Border border = new()
        {
            Background = new SolidColorBrush(Colors.White),
            BorderBrush = new SolidColorBrush(Colors.Black),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(4),
            Padding = new Thickness(8, 0, 8, 0),
        };

        border.SetBinding(Border.BorderBrushProperty, CreateFocusedBorderBrushBinding());

        return border;
    }

    private static Binding CreateFocusedBorderBrushBinding()
    {
        Binding binding = CreateTemplateBinding(nameof(TextBox.FocusState));
        binding.Converter = new FocusStateToBorderBrushConverter();

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

    private static Binding CreatePlaceholderVisibilityBinding()
    {
        Binding binding = CreateTemplateBinding(nameof(TextBox.Text));
        binding.Converter = new EmptyTextToVisibilityConverter();

        return binding;
    }


    private sealed class EmptyTextToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return string.IsNullOrEmpty(value as string) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }

    private sealed class FocusStateToBorderBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is FocusState.Unfocused
                ? new SolidColorBrush(Colors.Black)
                : new SolidColorBrush(Colors.DodgerBlue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
