using Uno.Extensions.Equality;
using Console = System.Console;

namespace Tracker.Frontend.Uno.Presentation;

public partial class ModulesNavigationViewModel : ObservableObject
{
    //private readonly INavigator _navigator;

    [ObservableProperty] private TrackerModule.Module activeModule;
    private Dictionary<TrackerModule.Module, Grid> contentGrids = new();

    public ModulesNavigationViewModel()
    {
        //_navigator = navigator;
        Title = "Tracker";
    }

    public string? Title { get; }

    public void ListViewOnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listView = (ListView) sender;
        TrackerModule trackerModule = ModulesNavigationPage.Modules[listView.SelectedIndex];

        SetActiveModule(trackerModule.TypeModule);
    }

    public void SetContentGridsDictionary(Dictionary<TrackerModule.Module, Grid> contentGrids)
    {
        this.contentGrids = contentGrids;
        SetActiveModule(ModulesNavigationPage.Modules[0].TypeModule);
    }

    private void SetActiveModule(TrackerModule.Module module)
    {
        ActiveModule = module;

        foreach (var grid in contentGrids)
        {
            grid.Value.Visibility = Visibility.Collapsed;
        }

        contentGrids[ActiveModule].Visibility = Visibility.Visible;

        Console.WriteLine($"Collapsed all grids except {ActiveModule}");

        //BudgetModuleVisibility =
        //    ActiveModule == TrackerModule.Module.BUDGET ? Visibility.Visible : Visibility.Collapsed;
        //DiningModuleVisibility =
        //    ActiveModule == TrackerModule.Module.DINING ? Visibility.Visible : Visibility.Collapsed;
        //TimeModuleVisibility = ActiveModule == TrackerModule.Module.TIME ? Visibility.Visible : Visibility.Collapsed;

        //Console.WriteLine(
        //    $"Budget: {BudgetModuleVisibility}; Dining: {DiningModuleVisibility}; Time: {TimeModuleVisibility}");
    }
}
