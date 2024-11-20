using Tracker.Module.Budget.Presentation.ViewModel;

namespace Tracker.Frontend.Uno.Presentation;

public partial class ModulesNavigationViewModel : ObservableObject
{
    private readonly INavigator _navigator;

    public ModulesNavigationViewModel(IOptions<AppConfig> appInfo, INavigator navigator)
    {
        _navigator = navigator;
        Title = "Tracker";

        //GoToSecond = new AsyncRelayCommand(GoToSecondView);
    }

    public string? Title { get; }

    //public ICommand GoToSecond { get; }
}
