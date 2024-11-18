using Microsoft.UI.Xaml.Controls;
using Tracker.Module.Budget.Presentation.ViewModel;
using Uno.Extensions.Navigation.UI;

namespace Tracker.Module.Budget.Presentation;

public sealed partial class BudgetTabs : UserControl
{
    public const string BUDGET_TAB_REGION_NAME_ONE = "PaymentsTab";

    public BudgetTabs()
    {
        this.DataContext<BudgetTabsViewModel>((userControl, vm) =>
            userControl.TabNavigation(KeyboardNavigationMode.Cycle).Background(Theme.Brushes.Background.Default)
                .Content(BuildContent(vm)));
    }

    private Grid BuildContent(BudgetTabsViewModel viewModel)
    {
        var grid = new Grid();

        grid.SafeArea(SafeArea.InsetMask.VisibleBounds);
        grid.RowDefinitions(new GridLength(8, GridUnitType.Star), new GridLength(92, GridUnitType.Star));

        TabBar tabBar = BuildTabBar(viewModel).Grid(row: 0);
        Grid contentGrid = BuildContentGrid(viewModel).Grid(row: 1);

        grid.Children.Add(tabBar);
        grid.Children.Add(contentGrid);

        return grid;
    }

    private TabBar BuildTabBar(BudgetTabsViewModel viewModel)
    {
        var tabBarItems = new TabBarItem[]
        {
            new() {Content = "Payments",},
        };

        var tabBar = new TabBar
        {
            VerticalAlignment = VerticalAlignment.Top,
            ItemsSource = tabBarItems,
        };

        return tabBar;
    }

    private Grid BuildContentGrid(BudgetTabsViewModel viewModel)
    {
        var grid = new Grid();

        var tabOne = new Grid() {Visibility = Visibility.Collapsed,};
        tabOne.Children.Add(new PaymentsTab());

        grid.Children.Add(tabOne);

        return grid;
    }
}
