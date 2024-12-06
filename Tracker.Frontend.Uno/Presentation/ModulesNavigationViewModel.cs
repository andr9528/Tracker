using Uno.Extensions.Equality;

namespace Tracker.Frontend.Uno.Presentation;

public partial class ModulesNavigationViewModel : ObservableObject
{
    //private readonly INavigator _navigator;

    [ObservableProperty] private TrackerModule.Module activeModule;
    [ObservableProperty] private Visibility budgetModuleVisibility;
    [ObservableProperty] private Visibility diningModuleVisibility;
    [ObservableProperty] private Visibility timeModuleVisibility;

    public ModulesNavigationViewModel()
    {
        //_navigator = navigator;
        Title = "Tracker";

        SetActiveModule(ModulesNavigationPage.Modules[0].TypeModule);
    }

    public string? Title { get; }

    public void ListViewOnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listView = (ListView) sender;
        TrackerModule trackerModule = ModulesNavigationPage.Modules[listView.SelectedIndex];

        SetActiveModule(trackerModule.TypeModule);
    }

    private void SetActiveModule(TrackerModule.Module module)
    {
        ActiveModule = module;
        BudgetModuleVisibility =
            ActiveModule == TrackerModule.Module.BUDGET ? Visibility.Visible : Visibility.Collapsed;
        DiningModuleVisibility =
            ActiveModule == TrackerModule.Module.DINING ? Visibility.Visible : Visibility.Collapsed;
        TimeModuleVisibility = ActiveModule == TrackerModule.Module.TIME ? Visibility.Visible : Visibility.Collapsed;
    }
}
