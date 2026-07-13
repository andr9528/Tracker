using Microsoft.UI;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Tracker.Shared.Frontend.Converters;

namespace Tracker.Shared.Frontend.Factory;

public static class ButtonFactory
{
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
