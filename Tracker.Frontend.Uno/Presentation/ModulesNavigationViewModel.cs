namespace Tracker.Frontend.Uno.Presentation;

public partial class ModulesNavigationViewModel : ObservableObject
{
    private readonly INavigator navigator;

    public ModulesNavigationViewModel(IOptions<AppConfig> appInfo, INavigator navigator)
    {
        this.navigator = navigator;

        Title = "Tracker";

        //GoToSecond = new AsyncRelayCommand(GoToSecondView);
    }

    public string? Title { get; }

    //public ICommand GoToSecond { get; }

    //private async Task GoToSecondView()
    //{
    //    //await _navigator.NavigateViewModelAsync<SecondViewModel>(this, data: new Entity(Name!));
    //}
}
