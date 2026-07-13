using Microsoft.UI.Input;
using Tracker.Frontend.Presentation;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Frontend;

public sealed partial class MainPage : Page
{
    private readonly ILogger<MainPage> logger;

    public MainPage()
    {
        logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<MainPage>>();

        Background = new SolidColorBrush(Colors.LightGray);

        Grid contentGrid = CreateGrid();
        AddElementsToGrid(contentGrid);

        PointerPressed += PointerPressedHandler;

        Content = contentGrid;
    }

    private void PointerPressedHandler(object sender, PointerRoutedEventArgs e)
    {
        try
        {
            PointerPointProperties properties = e.GetCurrentPoint(this).Properties;

            if (!properties.IsXButton1Pressed)
            {
                return;
            }

            var navigationService = App.Startup.ServiceProvider.GetRequiredService<INavigationService>();

            logger.LogDebug($"Back mouse button clicked - Attempting to Navigate Back.");
            navigationService.NavigateBack();

            e.Handled = true;
        }
        catch (Exception exe)
        {
            logger.LogError(exe, $"Exception caught during handling of Mouse click event.");
            throw;
        }
    }

    private void AddElementsToGrid(Grid contentGrid)
    {
        var selector = ActivatorUtilities.CreateInstance<PageSelector>(App.Startup.ServiceProvider);

        contentGrid.Children.Add(selector.SetRow(0).SetColumn(0));
    }

    private Grid CreateGrid()
    {
        Grid grid = GridFactory.CreateDefaultGrid();
        grid.Margin(new Thickness(0));

        grid.DefineRows(sizes: [100,]);
        grid.DefineColumns(sizes: [100,]);

        return grid;
    }
}
