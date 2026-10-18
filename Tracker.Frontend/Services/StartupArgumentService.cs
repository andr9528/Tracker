using Microsoft.UI.Windowing;

namespace Tracker.Frontend.Services;

public class StartupArgumentService
{
    private readonly Dictionary<string, Action<Window>> argumentHandlers;

    public StartupArgumentService(WindowTrayService windowTrayService)
    {
        argumentHandlers = new Dictionary<string, Action<Window>>(StringComparer.OrdinalIgnoreCase)
        {
            ["--minimize-to-tray"] = windowTrayService.EnableMinimizeToTray,
            ["--minimized"] = MinimizeWindow,
        };
    }

    public void ProcessArguments(Window mainWindow, LaunchActivatedEventArgs args)
    {
        var arguments =
            args.Arguments?.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet(StringComparer.OrdinalIgnoreCase) ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (string argument in arguments)
        {
            if (argumentHandlers.TryGetValue(argument, out var handler))
            {
                handler(mainWindow);
            }
        }
    }

    private void MinimizeWindow(Window window)
    {
        if (window.AppWindow.Presenter is OverlappedPresenter presenter)
        {
            presenter.Minimize();
        }
    }
}
