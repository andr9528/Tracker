namespace Tracker.Frontend.Uno.Presentation;

public sealed partial class ModulesNavigationPage : Page
{
    public TrackerModule[] Modules { get; } = new TrackerModule[]
    {
        new("Budget"),
        new("Dining"),
        new("Time"),
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
        grid.RowDefinitions(GridLength.Auto, GridLength.FromString("*"));

        NavigationBar navBar = BuildNavigationBar(viewModel).Grid(row: 0);
        Grid navGrid = BuildNavigationGrid(viewModel).Grid(row: 1);

        grid.Children.Add(navBar);
        grid.Children.Add(navGrid);

        return grid;
    }

    private NavigationBar BuildNavigationBar(ModulesNavigationViewModel viewModel)
    {
        var navBar = new NavigationBar();

        navBar.Content(() => viewModel.Title);

        return navBar;
    }

    private Grid BuildNavigationGrid(ModulesNavigationViewModel viewModel)
    {
        var grid = new Grid();

        grid.ColumnDefinitions(new GridLength(20, GridUnitType.Star), new GridLength(80, GridUnitType.Star));

        ListView listView = BuildNavigationListView(viewModel).Grid(row: 1);
        Grid controlGrid = BuildContentControlGrid(viewModel).Grid(1);

        grid.Children.Add(listView);
        grid.Children.Add(controlGrid);

        return grid;
    }

    private ListView BuildNavigationListView(ModulesNavigationViewModel viewModel)
    {
        var listView = new ListView();

        listView.SetBinding(ItemsControl.ItemsSourceProperty, nameof(Modules));

        var textBlockTemplate = new TextBlock();
        textBlockTemplate.SetBinding(TextBlock.TextProperty, nameof(TrackerModule.Name));

        listView.ItemTemplate = new DataTemplate(() => textBlockTemplate);

        return listView;
    }

    private Grid BuildContentControlGrid(ModulesNavigationViewModel viewModel)
    {
        var grid = new Grid
        {
            Background = new SolidColorBrush(Colors.AliceBlue),
        };

        var contentControl = new ContentControl
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalContentAlignment = HorizontalAlignment.Stretch,
            VerticalContentAlignment = VerticalAlignment.Stretch,
        };

        grid.Children.Add(contentControl);

        return grid;
    }
}
