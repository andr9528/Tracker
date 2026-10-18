using H.NotifyIcon;
using Microsoft.UI.Windowing;

namespace Tracker.Frontend.Services;

public sealed class WindowTrayService : IDisposable
{
    private readonly WindowsApiService windowsApiService;
    private bool isRestoring;

    private TaskbarIcon? trayIcon;
    private Window? trayWindow;

    public WindowTrayService(WindowsApiService windowsApiService)
    {
        this.windowsApiService = windowsApiService;
    }

    public void Dispose()
    {
        if (trayWindow != null)
        {
            trayWindow.AppWindow.Changed -= OnWindowChanged;
            trayWindow.Closed -= OnWindowClosed;
        }

        trayIcon?.Dispose();
        trayIcon = null;
        trayWindow = null;
    }

    public void EnableMinimizeToTray(Window window)
    {
        if (trayIcon != null)
        {
            return;
        }

        trayWindow = window;

        trayIcon = new TaskbarIcon
        {
            ToolTipText = "Tracker",
            IconSource = "ms-appx:///Assets/Icons/icon_foreground.ico",
            LeftClickCommand = new TrayCommand(RestoreWindow),
            NoLeftClickDelay = true,
        };

        trayIcon.ForceCreate(false);

        window.AppWindow.Changed += OnWindowChanged;
        window.Closed += OnWindowClosed;
    }

    private void OnWindowChanged(AppWindow sender, AppWindowChangedEventArgs args)
    {
        if (isRestoring)
        {
            return;
        }

        if (sender.Presenter is OverlappedPresenter presenter && presenter.State == OverlappedPresenterState.Minimized)
        {
            windowsApiService.HideMainWindow();
        }
    }

    private void RestoreWindow()
    {
        if (trayWindow == null)
        {
            return;
        }

        isRestoring = true;

        try
        {
            windowsApiService.ShowMainWindow();

            if (trayWindow.AppWindow.Presenter is OverlappedPresenter presenter)
            {
                presenter.Restore();
            }

            trayWindow.Activate();
        }
        finally
        {
            isRestoring = false;
        }
    }

    private void OnWindowClosed(object sender, WindowEventArgs args)
    {
        Dispose();
    }

    private sealed class TrayCommand(Action execute) : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            execute();
        }
    }
}
