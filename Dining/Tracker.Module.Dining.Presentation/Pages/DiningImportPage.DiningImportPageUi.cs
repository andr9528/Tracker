using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Tracker.Module.Dining.Abstraction.Records;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages;

internal sealed partial class DiningImportPage
{
    private sealed class DiningImportPageUi(DiningImportPageLogic logic, DiningImportPageViewModel viewModel)
        : BaseUi<DiningImportPageLogic, DiningImportPageViewModel>(logic, viewModel)
    {
        private const int CONTENT_MAX_WIDTH = 800;

        protected override void ConfigureGrid(Grid grid)
        {
            grid.Padding = new Thickness(16);
            grid.RowSpacing = 16;

            grid.DefineRows(GridLength.Auto, GridLength.Auto, GridLength.Auto, GridLength.Auto,
                new GridLength(1, GridUnitType.Star));

            grid.DefineColumns(new GridLength(1, GridUnitType.Star));
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(TextBlockFactory.CreateHeader("Import Dining Data").SetRow(0));

            grid.Children.Add(CreateFileSelectionSection().SetRow(1));

            grid.Children.Add(CreateStatusSection().SetRow(2));

            grid.Children.Add(CreateImportButton().SetRow(3));

            grid.Children.Add(CreateImportResultCard().SetRow(4));
        }

        private Grid CreateFileSelectionSection()
        {
            Grid grid = new()
            {
                ColumnSpacing = 12,
                MaxWidth = CONTENT_MAX_WIDTH,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };

            grid.DefineColumns(new GridLength(1, GridUnitType.Star), GridLength.Auto);

            grid.Children.Add(CreateSelectedFile().SetColumn(0));

            grid.Children.Add(CreateBrowseButton().SetColumn(1));

            return grid;
        }

        private UIElement CreateSelectedFile()
        {
            Grid grid = new()
            {
                RowSpacing = 4,
            };

            grid.DefineRows(GridLength.Auto, GridLength.Auto);

            TextBlock header = TextBlockFactory.CreateBlackText();
            header.Text = "Excel file";
            header.FontSize = 12;

            TextBlock filePath = TextBlockFactory.CreateBlackText();
            filePath.TextWrapping = TextWrapping.Wrap;

            filePath.SetBinding(TextBlock.TextProperty, new Binding
            {
                Path = new PropertyPath(nameof(DiningImportPageViewModel.SelectedFilePath)),
                Mode = BindingMode.OneWay,
            });

            grid.Children.Add(header.SetRow(0));
            grid.Children.Add(filePath.SetRow(1));

            return new Border
            {
                BorderBrush = new SolidColorBrush(Colors.Gray),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(8),
                Child = grid,
            };
        }

        private Button CreateBrowseButton()
        {
            Button button = ButtonFactory.CreateButton("Browse...", Logic.BrowseClicked);

            button.VerticalAlignment = VerticalAlignment.Bottom;

            return button;
        }

        private Border CreateStatusSection()
        {
            Grid content = new()
            {
                RowSpacing = 8,
            };

            content.DefineRows(GridLength.Auto, new GridLength(1, GridUnitType.Star));

            content.Children.Add(CreateSectionHeading("Status").SetRow(0));

            content.Children.Add(CreateStatusList().SetRow(1));

            return new Border
            {
                Background = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(Colors.LightGray),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(12),
                Padding = new Thickness(16),
                MinHeight = 140,
                MaxHeight = 240,
                MaxWidth = CONTENT_MAX_WIDTH,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Child = content,
            };
        }

        private ListView CreateStatusList()
        {
            ListView listView = new()
            {
                SelectionMode = ListViewSelectionMode.None,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                ItemTemplate = new DataTemplate(() =>
                {
                    TextBlock textBlock = TextBlockFactory.CreateBlackText();

                    textBlock.TextWrapping = TextWrapping.Wrap;

                    textBlock.SetBinding(TextBlock.TextProperty, new Binding
                    {
                        Mode = BindingMode.OneWay,
                    });

                    return textBlock;
                }),
            };

            listView.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Path = new PropertyPath(nameof(DiningImportPageViewModel.StatusMessages)),
                Mode = BindingMode.OneWay,
            });

            return listView;
        }

        private Button CreateImportButton()
        {
            Button button = ButtonFactory.CreateButton(Symbol.Upload, "Import", Logic.ImportClicked);

            button.MinWidth = 160;
            button.HorizontalAlignment = HorizontalAlignment.Center;

            button.SetBinding(IsEnabledProperty, new Binding
            {
                Path = new PropertyPath(nameof(DiningImportPageViewModel.CanImport)),
                Mode = BindingMode.OneWay,
            });

            return button;
        }

        private Border CreateImportResultCard()
        {
            Grid content = new()
            {
                RowSpacing = 12,
            };

            content.DefineRows(GridLength.Auto, GridLength.Auto, GridLength.Auto, GridLength.Auto, GridLength.Auto,
                GridLength.Auto, GridLength.Auto);

            content.Children.Add(CreateSectionHeading("Import Result").SetRow(0));

            content.Children.Add(CreateResultRow("Dinners created", nameof(ImportResult.CreatedDinners)).SetRow(1));

            content.Children.Add(CreateResultRow("Dinners Updated", nameof(ImportResult.UpdatedDinners)).SetRow(2));

            content.Children.Add(CreateResultRow("Invalid rows skipped", nameof(ImportResult.SkippedInvalidRows))
                .SetRow(3));

            content.Children.Add(CreateResultRow("Dishes created", nameof(ImportResult.CreatedDishes)).SetRow(4));

            content.Children.Add(CreateResultRow("Ingredients created", nameof(ImportResult.CreatedIngredients))
                .SetRow(5));

            content.Children.Add(
                CreateResultRow("Dish ingredients created", nameof(ImportResult.CreatedDishIngredients)).SetRow(6));

            return new Border
            {
                Background = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(Colors.LightGray),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(12),
                Padding = new Thickness(24),
                Margin = new Thickness(0, 8, 0, 0),
                MaxWidth = CONTENT_MAX_WIDTH,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Top,
                Child = content,
            };
        }

        private Grid CreateResultRow(string label, string resultProperty)
        {
            Grid grid = new()
            {
                ColumnSpacing = 16,
            };

            grid.DefineColumns(new GridLength(1, GridUnitType.Star), GridLength.Auto);

            grid.Children.Add(CreateResultLabel(label).SetColumn(0));

            grid.Children.Add(CreateResultValue(resultProperty).SetColumn(1));

            return grid;
        }

        private TextBlock CreateSectionHeading(string text)
        {
            TextBlock textBlock = TextBlockFactory.CreateBlackText();

            textBlock.Text = text;
            textBlock.FontSize = 18;
            textBlock.FontWeight = Microsoft.UI.Text.FontWeights.SemiBold;

            return textBlock;
        }

        private TextBlock CreateResultLabel(string label)
        {
            TextBlock textBlock = TextBlockFactory.CreateBlackText();

            textBlock.Text = label;
            textBlock.TextWrapping = TextWrapping.Wrap;

            return textBlock;
        }

        private TextBlock CreateResultValue(string resultProperty)
        {
            TextBlock textBlock = TextBlockFactory.CreateBlackText();

            textBlock.FontWeight = Microsoft.UI.Text.FontWeights.SemiBold;

            textBlock.SetBinding(TextBlock.TextProperty, new Binding
            {
                Path = new PropertyPath($"{nameof(DiningImportPageViewModel.Result)}.{resultProperty}"),
                Mode = BindingMode.OneWay,
            });

            return textBlock;
        }
    }
}
