using Tracker.Module.Budget.Presentation.ViewModel;

namespace Tracker.Frontend.Uno.Presentation;

public partial class ModulesNavigationViewModel : ObservableObject
{
    private readonly INavigator _navigator;
    [ObservableProperty] private TrackerModule activeModule;

    public ModulesNavigationViewModel(IOptions<AppConfig> appInfo, INavigator navigator)
    {
        _navigator = navigator;
        activeModule = ModulesNavigationPage.Modules.First();
        Title = "Tracker";

        //GoToSecond = new AsyncRelayCommand(GoToSecondView);
    }

    public void ListViewOnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listViewSender = (ListView) sender;
        int selectedIndex = listViewSender.SelectedIndex;
        ActiveModule = ModulesNavigationPage.Modules[selectedIndex];
    }

    public string? Title { get; }

    //public ICommand GoToSecond { get; }
}
