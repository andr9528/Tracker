using System.Drawing;
using Microsoft.UI.Xaml.Controls;
using Tracker.Module.Budget.Presentation.ViewModel;
using Uno.Extensions.Navigation.UI;

namespace Tracker.Module.Budget.Presentation;

public sealed partial class BudgetTabs : UserControl
{
    public const string TAB_REGION_NAME_ONE = "PaymentsTab";

    public BudgetTabs()
    {
        var ui = new BudgetTabsUi();

        this.DataContext<BudgetTabsViewModel>((userControl, vm) =>
            userControl.TabNavigation(KeyboardNavigationMode.Cycle).Background(Theme.Brushes.Background.Default)
                .Content(ui.BuildContent(vm)));
    }

    private class BudgetTabsUi
    {
        public Grid BuildContent(BudgetTabsViewModel viewModel)
        {
            var grid = new Grid();

            grid.SafeArea(SafeArea.InsetMask.VisibleBounds);
            grid.RowDefinitions(new GridLength(8, GridUnitType.Star), new GridLength(92, GridUnitType.Star));

            TabBar tabBar = BuildTabBar().Grid(row: 0);
            Grid contentGrid = BuildContentGrid().Grid(row: 1);

            grid.Children.Add(tabBar);
            grid.Children.Add(contentGrid);

            return grid;
        }

        private TabBar BuildTabBar()
        {
            var tabBarItems = new TabBarItem[]
            {
                new() {Content = "Payments",},
            };

            var tabBar = new TabBar
            {
                VerticalAlignment = VerticalAlignment.Top,
                ItemsSource = tabBarItems,
                Background = new SolidColorBrush(Colors.DarkGray),
            };

            return tabBar;
        }

        private Grid BuildContentGrid()
        {
            var grid = new Grid();

            var tabOne = new Grid() {Visibility = Visibility.Collapsed,};
            tabOne.Children.Add(new PaymentsTab());

            grid.Children.Add(tabOne);

            return grid;
        }
    }
}
