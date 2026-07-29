using Windows.UI;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Data;
using Tracker.Shared.Frontend.Extensions;

namespace Tracker.Shared.Frontend.Factory;

public static class SimplePieceFactory
{
    public static Color MenuBackgroundColour = Color.FromArgb(255, 32, 32, 32);

    public static Grid CreateFuzzySearchGrid(
        string useFuzzySearchPath, string searchModeTextPath, out CheckBox fuzzySearchCheckBox)
    {
        Grid grid = GridFactory.CreateDefaultGrid();

        grid.RowSpacing = 4;
        grid.ColumnSpacing = 8;
        grid.Margin = new Thickness(4);

        grid.DefineRows(GridLength.Auto, GridLength.Auto);
        grid.DefineColumns(GridLength.Auto, GridLength.Auto);

        fuzzySearchCheckBox = CheckBoxFactory.CreateLightCheckBox(useFuzzySearchPath);

        grid.Children.Add(CreateHeader().SetRow(0).SetColumn(0, 2));
        grid.Children.Add(fuzzySearchCheckBox.SetRow(1).SetColumn(0));
        grid.Children.Add(CreateSearchModeTextBlock(searchModeTextPath).SetRow(1).SetColumn(1));

        return grid;
    }

    private static TextBlock CreateHeader()
    {
        TextBlock header = TextBlockFactory.CreateBlackText("Use fuzzy search");
        header.Margin = new Thickness(4);

        return header;
    }

    private static TextBlock CreateSearchModeTextBlock(string bindingPath)
    {
        TextBlock textBlock = TextBlockFactory.CreateBlackText();
        textBlock.Margin = new Thickness(4);

        textBlock.SetBinding(TextBlock.TextProperty, new Binding
        {
            Path = new PropertyPath(bindingPath),
            Mode = BindingMode.OneWay,
        });

        return textBlock;
    }

    public static Grid CreateSaveCancelButtonGrid(
        Func<object, RoutedEventArgs, Task> saveClicked, RoutedEventHandler cancelClicked)
    {
        Grid grid = CreateActionButtonGrid(2);

        Button saveButton = CreateSaveButton(saveClicked);
        Button cancelButton = CreateCancelButton(cancelClicked);

        grid.Children.Add(saveButton.SetColumn(1));
        grid.Children.Add(cancelButton.SetColumn(2));

        return grid;
    }

    private static Button CreateSaveButton(Func<object, RoutedEventArgs, Task> saveClicked)
    {
        return ButtonFactory.CreateButton("Save", saveClicked);
    }

    private static Button CreateCancelButton(RoutedEventHandler cancelClicked)
    {
        return ButtonFactory.CreateButton("Cancel", cancelClicked);
    }

    public static Grid CreateLeftButtonGrid(string buttonText, RoutedEventHandler clicked)
    {
        Grid grid = GridFactory.CreateDefaultGrid()
            .DefineColumns(GridLength.Auto, new GridLength(1, GridUnitType.Star));

        Button button = ButtonFactory.CreateButton(buttonText, clicked, HorizontalAlignment.Left);

        grid.Children.Add(button.SetColumn(0));

        return grid;
    }

    public static Grid CreateFilterHeader()
    {
        Grid grid = GridFactory.CreateDefaultGrid();

        grid.DefineRows(new GridLength(1, GridUnitType.Star));

        TextBlock header = new()
        {
            Text = "Search & Filters",
            FontSize = 18,
            FontWeight = FontWeights.SemiBold,
            Foreground = new SolidColorBrush(Colors.Black),
            Margin = new Thickness(4, 0, 12, 0),
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        Border divider = new()
        {
            Height = 1,
            Background = new SolidColorBrush(MenuBackgroundColour),
            VerticalAlignment = VerticalAlignment.Bottom,
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };

        grid.Children.Add(header);
        grid.Children.Add(divider);

        return grid;
    }

    public static Grid CreateRightButtonGrid(string buttonText, RoutedEventHandler clicked)
    {
        Grid grid = GridFactory.CreateDefaultGrid();

        grid.DefineColumns(new GridLength(1, GridUnitType.Star), GridLength.Auto);

        Button button = ButtonFactory.CreateButton(buttonText, clicked, HorizontalAlignment.Right);

        grid.Children.Add(button.SetColumn(1));

        return grid;
    }

    public static Grid CreateThreeButtonGrid(
        string leftButtonText, RoutedEventHandler leftButtonClicked, string middleButtonText,
        RoutedEventHandler middleButtonClicked, string rightButtonText, RoutedEventHandler rightButtonClicked)
    {
        Grid grid = CreateActionButtonGrid(3);

        grid.Children.Add(ButtonFactory.CreateButton(leftButtonText, leftButtonClicked).SetColumn(1));

        grid.Children.Add(ButtonFactory.CreateButton(middleButtonText, middleButtonClicked).SetColumn(2));

        grid.Children.Add(ButtonFactory.CreateButton(rightButtonText, rightButtonClicked).SetColumn(3));

        return grid;
    }

    private static Grid CreateActionButtonGrid(int buttonCount)
    {
        Grid grid = GridFactory.CreateDefaultGrid();

        grid.HorizontalAlignment = HorizontalAlignment.Stretch;
        grid.VerticalAlignment = VerticalAlignment.Center;
        grid.ColumnSpacing = 8;

        grid.DefineColumns(new GridLength(1, GridUnitType.Star));

        for (var index = 0; index < buttonCount; index++)
        {
            grid.DefineColumns(GridLength.Auto);
        }

        return grid;
    }
}
