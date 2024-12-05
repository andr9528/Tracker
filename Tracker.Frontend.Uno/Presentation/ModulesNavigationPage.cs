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
        //listView.Region(true);

        var listOptions = Modules.Select(module =>
        {
            var block = new TextBlock()
            {
                Text = module.GetModuleAsReadableString(),
                Padding = new Thickness(10, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = listView.Background,
                FocusVisualPrimaryBrush = listView.Background,
                FocusVisualSecondaryBrush = listView.Background,
            };
            //button.Region(name: module.GetModuleAsReadableString());
            //button.SetRequest(module.GetModuleAsReadableString());
            //button.SetData(module.GetModuleControl());

            //button.Command(() => GetModuleCommand(viewModel, module.TypeModule));
            //button.Style(new Style(typeof(TextBlock)));

            return block;
        });

        listView.SelectionChanged += (sender, args) => ListViewOnSelectionChanged(sender, args, viewModel);
        listView.ItemsSource = listOptions;

        return listView;
    }

    private void ListViewOnSelectionChanged(
        object sender, SelectionChangedEventArgs e, ModulesNavigationViewModel viewModel)
    {
        Console.WriteLine($"Listview Selection Changed Called");
        viewModel.ListViewOnSelectionChanged(sender, e);
        // Not Fired...
        Console.WriteLine($"View Model Called");
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
            contentGrid.Visibility(() => GetModuleVisibility(viewModel, module.TypeModule));
            contentGrid.Region(name: module.GetModuleAsReadableString());
            contentGrid.Children.Add(module.GetModuleControl());

            grid.Children.Add(contentGrid);
        }

        return grid;
    }

    private Visibility GetModuleVisibility(ModulesNavigationViewModel viewModel, TrackerModule.Module module)
    {
        return module switch
        {
            TrackerModule.Module.TIME => viewModel.TimeModuleVisibility,
            TrackerModule.Module.DINING => viewModel.DiningModuleVisibility,
            TrackerModule.Module.BUDGET => viewModel.BudgetModuleVisibility,
            _ => viewModel.BudgetModuleVisibility,
        };
    }
}
