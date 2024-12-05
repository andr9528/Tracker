using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Tracker.Module.Dining.Presentation.ViewModel;

namespace Tracker.Module.Dining.Presentation;

public sealed partial class DiningTabs : UserControl
{
    public const string TAB_REGION_NAME_ONE = "DiningTabOne";

    public DiningTabs()
    {
        this.DataContext<DiningTabsViewModel>((userControl, vm) =>
            userControl.TabNavigation(KeyboardNavigationMode.Cycle).Background(Theme.Brushes.Background.Default)
                .Content(BuildContent(vm)));
    }

    private Grid BuildContent(DiningTabsViewModel viewModel)
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

    private TabBar BuildTabBar(DiningTabsViewModel viewModel)
    {
        var tabBarItems = new TabBarItem[]
        {
            new() {Content = TAB_REGION_NAME_ONE,},
        };

        var tabBar = new TabBar
        {
            VerticalAlignment = VerticalAlignment.Top,
            ItemsSource = tabBarItems,
            Background = new SolidColorBrush(Colors.DarkGray),
        };

        return tabBar;
    }

    private Grid BuildContentGrid(DiningTabsViewModel viewModel)
    {
        var grid = new Grid();

        var tabOne = new Grid() {Visibility = Visibility.Collapsed,};
        //tabOne.Children.Add(new PaymentsTab());

        grid.Children.Add(tabOne);

        return grid;
    }
}
