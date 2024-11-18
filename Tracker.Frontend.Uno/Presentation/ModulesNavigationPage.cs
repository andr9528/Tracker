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
        ListView navGrid = BuildNavigationListView(viewModel).Grid(row: 1, column: 0);
        Grid controlGrid = BuildContentControlGrid(viewModel).Grid(row: 0, column: 1, rowSpan: 2);

        grid.Children.Add(navBar);
        grid.Children.Add(navGrid);
        grid.Children.Add(controlGrid);

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

        var listOptions = Modules.Select(x => new TextBlock()
        {
            Text = x.GetModuleAsReadableString(),
            Margin = new Thickness(10, 0, 0, 0),
        });

        listView.ItemsSource = listOptions;
        // Viewmodel is null at this point during setup...
        //listView.SelectionChanged += viewModel.ListViewOnSelectionChanged;

        return listView;
    }


    private Grid BuildContentControlGrid(ModulesNavigationViewModel viewModel)
    {
        var grid = new Grid
        {
            Background = new SolidColorBrush(Colors.DarkOrange),
        };

        var contentControl = new ContentControl
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalContentAlignment = HorizontalAlignment.Stretch,
            VerticalContentAlignment = VerticalAlignment.Stretch,
        };
        contentControl.Content(x =>
            x.Binding(() => viewModel.ActiveModule.GetModuleControl())
                .UpdateSourceTrigger(UpdateSourceTrigger.PropertyChanged));

        grid.Children.Add(contentControl);

        return grid;
    }
}
