namespace Tracker.Frontend.Uno.Presentation;

public class ShellViewModel
{
    private readonly INavigator _navigator;

    public ShellViewModel(INavigator navigator)
    {
        _navigator = navigator;
        // Add code here to initialize or attach event handlers to singleton services
        _ = Start();
    }

    private async Task Start()
    {
        await _navigator.NavigateViewModelAsync<ModulesNavigationViewModel>(this);
    }
}
