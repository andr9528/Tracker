using Microsoft.UI;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Tracker.Shared.Frontend.Converters;

namespace Tracker.Shared.Frontend.Factory;

public static class ButtonFactory
{
    public static Button CreateButton(
        string text, RoutedEventHandler clicked, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center)
    {
        Button button = CreateBaseButton(text, horizontalAlignment);

        button.Click += clicked;

        return button;
    }

    private static Button CreateBaseButton(object content, HorizontalAlignment horizontalAlignment)
    {
        return new Button
        {
            Content = content,
            Padding = new Thickness(20, 8, 20, 8),
            HorizontalAlignment = horizontalAlignment,
            VerticalAlignment = VerticalAlignment.Center,
        };
    }

    public static Button CreateButton(
        string text, Func<object, RoutedEventArgs, Task> clicked,
        HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center)
    {
        Button button = CreateBaseButton(text, horizontalAlignment);

        button.Click += async (sender, args) => await clicked(sender, args);

        return button;
    }

    public static Button CreateButton(
        Symbol symbol, string text, RoutedEventHandler clicked,
        HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center)
    {
        Button button = CreateBaseButton(CreateButtonContent(symbol, text), horizontalAlignment);

        button.Click += clicked;

        return button;
    }

    public static Button CreateButton(
        string text, Symbol trailingSymbol, RoutedEventHandler clicked,
        HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center)
    {
        Button button = CreateBaseButton(CreateButtonContent(text, trailingSymbol), horizontalAlignment);

        button.Click += clicked;

        return button;
    }

    private static StackPanel CreateButtonContent(Symbol symbol, string text)
    {
        return new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 10,
            Children =
            {
                new SymbolIcon(symbol)
                {
                    VerticalAlignment = VerticalAlignment.Center,
                },
                new TextBlock
                {
                    Text = text,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            },
        };
    }

    private static StackPanel CreateButtonContent(string text, Symbol trailingSymbol)
    {
        return new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 10,
            Children =
            {
                new TextBlock
                {
                    Text = text,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                new SymbolIcon(trailingSymbol)
                {
                    VerticalAlignment = VerticalAlignment.Center,
                },
            },
        };
    }

    public static HyperlinkButton BuildHyperlinkButton(string url)
    {
        Uri.TryCreate(url, UriKind.Absolute, out Uri? uri);

        return new HyperlinkButton
        {
            NavigateUri = uri,
            IsEnabled = uri is not null,
            VerticalAlignment = VerticalAlignment.Center,
            Content = new TextBlock
            {
                Text = url,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
            },
        };
    }

    public static RadioButton CreateNullableBooleanOptionButton(string text)
    {
        return new RadioButton
        {
            Content = text,
            Foreground = new SolidColorBrush(Colors.Black),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            VerticalContentAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0),
            Padding = new Thickness(0),
            Template = CreateNullableBooleanOptionButtonTemplate(),
        };
    }

    private static ControlTemplate CreateNullableBooleanOptionButtonTemplate()
    {
        return new ControlTemplate(() =>
        {
            Border border = CreateNullableBooleanOptionButtonBorder();

            ContentPresenter content = new()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            border.Child = content;

            return border;
        });
    }

    private static Border CreateNullableBooleanOptionButtonBorder()
    {
        Border border = new()
        {
            Background = new SolidColorBrush(Colors.White),
            BorderBrush = new SolidColorBrush(Colors.Black),
            BorderThickness = new Thickness(1),
            Padding = new Thickness(8, 0, 8, 0),
        };

        border.SetBinding(FrameworkElement.BackgroundProperty, CreateCheckedBackgroundBinding());
        border.SetBinding(Border.BorderBrushProperty, CreateCheckedBorderBrushBinding());

        return border;
    }

    private static Binding CreateCheckedBackgroundBinding()
    {
        Binding binding = CreateTemplateBinding(nameof(ToggleButton.IsChecked));
        binding.Converter = new CheckedToBackgroundBrushConverter();

        return binding;
    }

    private static Binding CreateCheckedBorderBrushBinding()
    {
        Binding binding = CreateTemplateBinding(nameof(ToggleButton.IsChecked));
        binding.Converter = new CheckedToBorderBrushConverter();

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
}
