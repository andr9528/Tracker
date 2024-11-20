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
        this.DataContext<ModulesNavigationViewModel>((page, vm) =>
            page.NavigationCacheMode(NavigationCacheMode.Required).Background(Theme.Brushes.Background.Default)
                .Content(BuildContent(vm)));
    }

    private Grid BuildContent(ModulesNavigationViewModel viewModel)
    {
        var grid = new Grid();

        grid.SafeArea(SafeArea.InsetMask.VisibleBounds);
        grid.RowDefinitions(new GridLength(8, GridUnitType.Star), new GridLength(92, GridUnitType.Star));
        grid.ColumnDefinitions(new GridLength(10, GridUnitType.Star), new GridLength(90, GridUnitType.Star));

        NavigationBar navBar = BuildNavigationBar(viewModel).Grid(row: 0, column: 0);
        ListView navList = BuildNavigationListView(viewModel).Grid(row: 1, column: 0);
        Grid contentGrid = BuildContentGrid(viewModel).Grid(row: 0, column: 1, rowSpan: 2);

        grid.Children.Add(navBar);
        grid.Children.Add(navList);
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
        var listView = new ListView();
        listView.Region(true);

        var listOptions = Modules.Select(module =>
        {
            var block = new TextBlock()
            {
                Text = module.GetModuleAsReadableString(),
                Margin = new Thickness(10, 0, 0, 0),
            };
            block.Region(name: module.GetModuleAsReadableString());

            return block;
        });

        listView.ItemsSource = listOptions;

        return listView;
    }


    private Grid BuildContentGrid(ModulesNavigationViewModel viewModel)
    {
        var grid = new Grid
        {
            Background = new SolidColorBrush(Colors.AliceBlue),
        };
        grid.Region(true, navigator: "Visibility");

        foreach (TrackerModule module in Modules)
        {
            var contentGrid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Visibility = Visibility.Collapsed,
            };
            contentGrid.Region(name: module.GetModuleAsReadableString());
            contentGrid.Children.Add(module.GetModuleControl());

            grid.Children.Add(contentGrid);
        }

        return grid;
    }
}
