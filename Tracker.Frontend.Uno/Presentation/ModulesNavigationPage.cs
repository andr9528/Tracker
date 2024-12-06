using Microsoft.UI.Xaml;
using TextBlock = Microsoft.UI.Xaml.Controls.TextBlock;

namespace Tracker.Frontend.Uno.Presentation;

public sealed partial class ModulesNavigationPage : Page
{
    public static readonly List<TrackerModule> Modules = new()
    {
        new TrackerModule(TrackerModule.Module.BUDGET),
        new TrackerModule(TrackerModule.Module.DINING),
        new TrackerModule(TrackerModule.Module.TIME),
    };

    public ModulesNavigationPage()
    {
        DataContext ??= (Application.Current as App).Host.Services.GetRequiredService<ModulesNavigationViewModel>();

        this.NavigationCacheMode(NavigationCacheMode.Required).Background(Theme.Brushes.Background.Default).Content(
            BuildContent((ModulesNavigationViewModel) DataContext));
    }

    private Grid BuildContent(ModulesNavigationViewModel viewModel)
    {
        var grid = new Grid();

        const int rowOneHeight = 8;
        const int rowTwoHeight = 100 - rowOneHeight;
        const int columnOneWidth = 10;
        const int columnTwoWidth = 100 - columnOneWidth;

        grid.SafeArea(SafeArea.InsetMask.VisibleBounds);
        grid.RowDefinitions(new GridLength(rowOneHeight, GridUnitType.Star),
            new GridLength(rowTwoHeight, GridUnitType.Star));
        grid.ColumnDefinitions(new GridLength(columnOneWidth, GridUnitType.Star),
            new GridLength(columnTwoWidth, GridUnitType.Star));

        NavigationBar navigationBar = BuildNavigationBar(viewModel).Grid(row: 0, column: 0);
        ListView navigation = BuildNavigationListView(viewModel).Grid(row: 1, column: 0);
        Grid contentGrid = BuildContentGrid(viewModel).Grid(row: 0, column: 1, rowSpan: 2);

        grid.Children.Add(navigationBar);
        grid.Children.Add(navigation);
        grid.Children.Add(contentGrid);

        return grid;
    }

    private NavigationBar BuildNavigationBar(ModulesNavigationViewModel viewModel)
    {
        var navBar = new NavigationBar();

        navBar.Content(() => viewModel.Title);

        return navBar;
    }

    private ListView BuildNavigationListView(ModulesNavigationViewModel viewModel)
    {
        var view = new ListView();

        var options = Modules.Select(module =>
        {
            var item = new TextBlock()
            {
                Text = module.GetModuleAsReadableString(),
                Padding = new Thickness(10, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = view.Background,
                FocusVisualPrimaryBrush = view.Background,
                FocusVisualSecondaryBrush = view.Background,
            };

            return item;
        });

        view.SelectionChanged += (sender, args) => ListViewOnSelectionChanged(sender, args, viewModel);
        view.ItemsSource = options;

        return view;
    }

    private void ListViewOnSelectionChanged(
        object sender, SelectionChangedEventArgs e, ModulesNavigationViewModel viewModel)
    {
        viewModel.ListViewOnSelectionChanged(sender, e);
    }

    private Grid BuildContentGrid(ModulesNavigationViewModel viewModel)
    {
        var grid = new Grid
        {
            Background = new SolidColorBrush(Colors.AliceBlue),
        };

        Dictionary<TrackerModule.Module, Grid> contentGrids = new();

        foreach (TrackerModule module in Modules)
        {
            var contentGrid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Visibility = Visibility.Collapsed,
            };
            contentGrid.Children.Add(module.GetModuleControl());

            contentGrids.Add(module.TypeModule, contentGrid);
            grid.Children.Add(contentGrid);
        }

        viewModel.SetContentGridsDictionary(contentGrids);

        return grid;
    }
}
