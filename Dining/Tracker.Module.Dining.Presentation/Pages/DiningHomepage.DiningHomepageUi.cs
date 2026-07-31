using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
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

            grid.DefineRows(GridLength.Auto, new GridLength(1, GridUnitType.Star));

            grid.DefineColumns(new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star));
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(TextBlockFactory.CreateHeader("Dining").SetRow(0).SetColumn(0, 2));

            grid.Children.Add(CreateLeftSection().SetRow(1).SetColumn(0));

            grid.Children.Add(CreateRightSection().SetRow(1).SetColumn(1));
        }

        private Grid CreateLeftSection()
        {
            Grid grid = new()
            {
                RowSpacing = 16,
                ColumnSpacing = 16,
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            grid.DefineRows(GridLength.Auto, GridLength.Auto, new GridLength(1, GridUnitType.Star));

            grid.DefineColumns(GridLength.Auto, GridLength.Auto, new GridLength(1, GridUnitType.Star));

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

        private Grid CreateRightSection()
        {
            Grid grid = new()
            {
                RowSpacing = 16,
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            grid.DefineRows(GridLength.Auto, GridLength.Auto, new GridLength(1, GridUnitType.Star), GridLength.Auto);

            grid.Children.Add(CreateStatisticCard("Most Eaten Dish", nameof(DiningHomepageViewModel.MostEatenDish))
                .SetRow(0));

            grid.Children.Add(CreateStatisticCard("Least Eaten Dish", nameof(DiningHomepageViewModel.LeastEatenDish))
                .SetRow(1));

            grid.Children.Add(CreateMoreStatisticsButton().SetRow(3));

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

        private Border CreateStatisticCard(string heading, string bindingPath)
        {
            Grid content = new()
            {
                RowSpacing = 16,
            };

            content.DefineRows(GridLength.Auto, new GridLength(1, GridUnitType.Star));

            content.Children.Add(CreateStatisticHeading(heading).SetRow(0));

            content.Children.Add(CreateStatisticValue(bindingPath).SetRow(1));

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

        private TextBlock CreateStatisticValue(string bindingPath)
        {
            TextBlock textBlock = TextBlockFactory.CreateBlackText();

            textBlock.FontSize = 18;
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
