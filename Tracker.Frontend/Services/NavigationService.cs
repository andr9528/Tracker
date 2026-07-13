using System.Diagnostics;
using Tracker.Shared.Frontend.Abstraction;

namespace Tracker.Frontend.Services;

public class NavigationService(IUiDispatcher uiDispatcher, ILogger<NavigationService> logger) : INavigationService
{
    private Frame? contentFrame;
    private readonly Stack<(UIElement Element, string Name)> navigationStack = new();

    /// <inheritdoc />
    public void RegisterContentFrame(Frame frame)
    {
        contentFrame = frame;
    }

    /// <inheritdoc />
    public void NavigateTo(UIElement element, string name)
    {
        navigationStack.Push((element, name));

        TimeSpan elapsed = UpdateFrameToTopElement();

        logger.LogDebug("Navigated to {PageName} in {ElapsedMilliseconds} ms", name, elapsed.TotalMilliseconds);
    }

    /// <inheritdoc />
    public void NavigateBack()
    {
        if (navigationStack.Count <= 1)
        {
            return;
        }

        string previousPageName = navigationStack.Peek().Name;

        navigationStack.Pop();
        PopSkippedPage();

        TimeSpan elapsed = UpdateFrameToTopElement();
        string currentPageName = navigationStack.Peek().Name;

        logger.LogDebug("Navigated back from {PreviousPageName} to {CurrentPageName} in {ElapsedMilliseconds} ms",
            previousPageName, currentPageName, elapsed.TotalMilliseconds);
    }

    private void PopSkippedPage()
    {
        if (navigationStack.Count <= 1)
        {
            return;
        }

        UIElement element = navigationStack.Peek().Element;

        return;

        //if (element is not SomeSkippedPage)
        //{
        //    return;
        //}

        navigationStack.Pop();
    }

    private TimeSpan UpdateFrameToTopElement()
    {
        var stopwatch = Stopwatch.StartNew();

        (UIElement peekedElement, string requestedPageName) = navigationStack.Peek();

        uiDispatcher.TryEnqueue(() =>
        {
            contentFrame?.Content = peekedElement;

            if (peekedElement is INavigationRefreshable refreshable)
            {
                refreshable.RefreshAfterNavigation();
            }
        });

        stopwatch.Stop();

        logger.LogInformation(
            "Navigated to {DisplayedPageName} (requested: {RequestedPageName}) in {ElapsedMilliseconds} ms",
            requestedPageName, requestedPageName, stopwatch.Elapsed.TotalMilliseconds);

        return stopwatch.Elapsed;
    }
}
