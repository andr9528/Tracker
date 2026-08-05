using Windows.UI.Text;
using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Tracker.Module.Dining.Abstraction.Records;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages;

internal sealed partial class DiningHomepage
{
    internal sealed class DiningHomepageUi(DiningHomepageLogic logic, DiningHomepageViewModel viewModel)
        : BaseUi<DiningHomepageLogic, DiningHomepageViewModel>(logic, viewModel)
    {
        private const int BUTTONS_MIN_WIDTH = 180;
        private const int STATISTIC_CARD_MIN_HEIGHT = 160;

        protected override void ConfigureGrid(Grid grid)
        {
            grid.Padding = new Thickness(16);
            grid.RowSpacing = 16;
            grid.ColumnSpacing = 24;

            grid.DefineRows(GridLength.Auto, GridLength.Auto, GridLength.Auto, GridLength.Auto,
                new GridLength(1, GridUnitType.Star), GridLength.Auto);

            grid.DefineColumns(new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star));
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            PlaceHeader(grid);
            PlaceNavigation(grid);
            PlaceStatisticsCards(grid);
            PlaceButtons(grid);
        }

        private void PlaceHeader(Grid grid)
        {
            grid.Children.Add(TextBlockFactory.CreateHeader("Dining").SetRow(0).SetColumn(0, 2));
        }

        private void PlaceNavigation(Grid grid)
        {
            grid.Children.Add(CreateNavigationSection().SetRow(1).SetColumn(0));
        }

        private void PlaceButtons(Grid grid)
        {
            grid.Children.Add(CreateMoreStatisticsButton().SetRow(5).SetColumn(1));
        }

        private void PlaceStatisticsCards(Grid grid)
        {
            grid.Children.Add(CreateDishStatisticsCard(
                "Top 3 Most Eaten Dishes", nameof(DiningHomepageViewModel.MostEatenDishes)).SetRow(1).SetColumn(1));

            grid.Children.Add(
                CreateSingleValueStatisticCard("Unique Dishes Eaten", nameof(DiningHomepageViewModel.UniqueDishesEaten))
                    .SetRow(2).SetColumn(0));

            grid.Children.Add(CreateDishStatisticsCard(
                "Top 3 Least Eaten Dishes", nameof(DiningHomepageViewModel.LeastEatenDishes)).SetRow(2).SetColumn(1));

            grid.Children.Add(CreateMostUsedIngredientsCard().SetRow(3).SetColumn(0));

            grid.Children.Add(CreateDishStatisticCard("Most Recently Added Dish",
                nameof(DiningHomepageViewModel.MostRecentlyAddedDishName),
                nameof(DiningHomepageViewModel.MostRecentlyAddedDishDetails)).SetRow(3).SetColumn(1));
        }

        private Border CreateDishStatisticsCard(string heading, string itemsSourceBindingPath)
        {
            Grid content = new()
            {
                RowSpacing = 12,
            };

            content.DefineRows(GridLength.Auto, GridLength.Auto);

            content.Children.Add(CreateStatisticHeading(heading).SetRow(0));

            content.Children.Add(CreateDishStatisticsList(itemsSourceBindingPath).SetRow(1));

            return CreateStatisticCardBorder(content);
        }

        private ItemsControl CreateDishStatisticsList(string itemsSourceBindingPath)
        {
            ItemsControl itemsControl = new()
            {
                ItemTemplate = new DataTemplate(() =>
                {
                    Grid row = new()
                    {
                        RowSpacing = 4,
                        Margin = new Thickness(0, 4),
                    };

                    row.DefineRows(GridLength.Auto, GridLength.Auto);

                    row.Children.Add(CreateDishName().SetRow(0));
                    row.Children.Add(CreateDishDetails().SetRow(1));

                    return row;
                }),
            };

            itemsControl.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Path = new PropertyPath(itemsSourceBindingPath),
                Mode = BindingMode.OneWay,
            });

            return itemsControl;
        }

        private TextBlock CreateDishName()
        {
            TextBlock textBlock = TextBlockFactory.CreateBlackText();

            textBlock.FontSize = 18;
            textBlock.FontWeight = Microsoft.UI.Text.FontWeights.SemiBold;
            textBlock.TextWrapping = TextWrapping.Wrap;

            textBlock.SetBinding(TextBlock.TextProperty, new Binding
            {
                Path = new PropertyPath(nameof(DishEatingStatistic.DishName)),
                Mode = BindingMode.OneWay,
            });

            return textBlock;
        }

        private TextBlock CreateDishDetails()
        {
            TextBlock textBlock = TextBlockFactory.CreateBlackText();

            textBlock.FontSize = 14;
            textBlock.TextWrapping = TextWrapping.Wrap;

            textBlock.SetBinding(TextBlock.TextProperty, new Binding
            {
                Path = new PropertyPath(nameof(DishEatingStatistic.Details)),
                Mode = BindingMode.OneWay,
            });

            return textBlock;
        }

        private Border CreateMostUsedIngredientsCard()
        {
            Grid content = new()
            {
                RowSpacing = 12,
            };

            content.DefineRows(GridLength.Auto, GridLength.Auto);

            content.Children.Add(CreateStatisticHeading("Top 3 Used Ingredients").SetRow(0));

            content.Children.Add(CreateMostUsedIngredientsList().SetRow(1));

            return CreateStatisticCardBorder(content);
        }

        private ItemsControl CreateMostUsedIngredientsList()
        {
            ItemsControl itemsControl = new()
            {
                ItemTemplate = new DataTemplate(() =>
                {
                    Grid row = new()
                    {
                        ColumnSpacing = 12,
                        Margin = new Thickness(0, 4),
                    };

                    row.DefineColumns(new GridLength(1, GridUnitType.Star), GridLength.Auto);

                    row.Children.Add(CreateIngredientName().SetColumn(0));

                    row.Children.Add(CreateIngredientUsage().SetColumn(1));

                    return row;
                }),
            };

            itemsControl.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Path = new PropertyPath(nameof(DiningHomepageViewModel.MostUsedIngredients)),
                Mode = BindingMode.OneWay,
            });

            return itemsControl;
        }

        private TextBlock CreateIngredientName()
        {
            TextBlock textBlock = TextBlockFactory.CreateBlackText();

            textBlock.TextWrapping = TextWrapping.Wrap;

            textBlock.SetBinding(TextBlock.TextProperty, new Binding
            {
                Path = new PropertyPath(nameof(IngredientUsageStatistic.IngredientName)),
                Mode = BindingMode.OneWay,
            });

            return textBlock;
        }

        private TextBlock CreateIngredientUsage()
        {
            Run dinnerCount = new();

            dinnerCount.SetBinding(Run.TextProperty, new Binding
            {
                Path = new PropertyPath(nameof(IngredientUsageStatistic.DinnerCount)),
                Mode = BindingMode.OneWay,
            });

            return new TextBlock
            {
                Inlines =
                {
                    new Run
                    {
                        Text = "Used in ",
                    },
                    dinnerCount,
                    new Run
                    {
                        Text = " dinners",
                    },
                },
            };
        }


        private Grid CreateNavigationSection()
        {
            Grid grid = new()
            {
                RowSpacing = 16,
                ColumnSpacing = 16,
            };

            grid.DefineColumns(GridLength.Auto, GridLength.Auto, new GridLength(1, GridUnitType.Star));

            grid.DefineRows(GridLength.Auto, GridLength.Auto);

            grid.Children.Add(CreateNavigationButton(Symbol.Download, "Import Data", Logic.ImportDataClicked).SetRow(0)
                .SetColumn(0));

            grid.Children.Add(CreateNavigationButton(Symbol.List, "Ingredients", Logic.IngredientsClicked).SetRow(0)
                .SetColumn(1));

            grid.Children.Add(CreateNavigationButton(Symbol.Calendar, "Dinners", Logic.DinnersClicked).SetRow(1)
                .SetColumn(0));

            grid.Children.Add(CreateNavigationButton(Symbol.AllApps, "Dishes", Logic.DishesClicked).SetRow(1)
                .SetColumn(1));

            return grid;
        }

        private Button CreateNavigationButton(Symbol symbol, string text, RoutedEventHandler clicked)
        {
            Button button = ButtonFactory.CreateButton(symbol, text, clicked, HorizontalAlignment.Left);

            button.MinWidth = BUTTONS_MIN_WIDTH;

            return button;
        }

        private Button CreateMoreStatisticsButton()
        {
            Button button = ButtonFactory.CreateButton("More Statistics", Symbol.Forward, Logic.MoreStatisticsClicked,
                HorizontalAlignment.Right);

            button.MinWidth = BUTTONS_MIN_WIDTH;

            return button;
        }

        private Border CreateDishStatisticCard(string heading, string nameBindingPath, string detailsBindingPath)
        {
            Grid content = new()
            {
                RowSpacing = 8,
            };

            content.DefineRows(GridLength.Auto, GridLength.Auto, GridLength.Auto);

            content.Children.Add(CreateStatisticHeading(heading).SetRow(0));

            content.Children.Add(CreateStatisticValue(nameBindingPath, 20, Microsoft.UI.Text.FontWeights.SemiBold)
                .SetRow(1));

            content.Children.Add(CreateStatisticValue(detailsBindingPath, 16, Microsoft.UI.Text.FontWeights.Normal)
                .SetRow(2));

            return CreateStatisticCardBorder(content);
        }

        private Border CreateSingleValueStatisticCard(string heading, string bindingPath)
        {
            Grid content = new()
            {
                RowSpacing = 16,
            };

            content.DefineRows(GridLength.Auto, GridLength.Auto);

            content.Children.Add(CreateStatisticHeading(heading).SetRow(0));

            content.Children.Add(
                CreateStatisticValue(bindingPath, 28, Microsoft.UI.Text.FontWeights.SemiBold).SetRow(1));

            return CreateStatisticCardBorder(content);
        }

        private Border CreateStatisticCardBorder(Grid content)
        {
            return new Border
            {
                Background = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(Colors.LightGray),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(12),
                Padding = new Thickness(24),
                MinHeight = STATISTIC_CARD_MIN_HEIGHT,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Child = content,
            };
        }

        private TextBlock CreateStatisticHeading(string heading)
        {
            TextBlock textBlock = TextBlockFactory.CreateBlackText();

            textBlock.Text = heading;
            textBlock.FontSize = 20;
            textBlock.FontWeight = Microsoft.UI.Text.FontWeights.SemiBold;
            textBlock.TextWrapping = TextWrapping.Wrap;

            return textBlock;
        }

        private TextBlock CreateStatisticValue(string bindingPath, double fontSize, FontWeight fontWeight)
        {
            TextBlock textBlock = TextBlockFactory.CreateBlackText();

            textBlock.FontSize = fontSize;
            textBlock.FontWeight = fontWeight;
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.VerticalAlignment = VerticalAlignment.Center;

            textBlock.SetBinding(TextBlock.TextProperty, new Binding
            {
                Path = new PropertyPath(bindingPath),
                Mode = BindingMode.OneWay,
            });

            return textBlock;
        }
    }
}
