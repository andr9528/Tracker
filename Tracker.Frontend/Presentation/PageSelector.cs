using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Services.Configuration;

namespace Tracker.Frontend.Presentation;

/// <summary>
/// Page navigation menu that hosts "regions" (pages) and swaps Content based on selection.
/// </summary>
internal sealed partial class PageSelector : Page
{
    public PageSelector(
        IServiceProvider serviceProvider, IEnumerable<IPageRegion> regionDefinitions,
        INavigationService navigationService, ConfigurationService configurationService)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(regionDefinitions);
        ArgumentNullException.ThrowIfNull(navigationService);

        DataContext = new PageSelectorViewModel();

        Margin = new Thickness(0);

        var viewModel = (PageSelectorViewModel) DataContext;
        var logic = new PageSelectorLogic(viewModel, serviceProvider, navigationService, configurationService);
        var ui = new PageSelectorUi(logic, viewModel, regionDefinitions, navigationService);

        Content = ui.CreateContentGrid();

        logic.NavigateToFirstRegion();
    }
}
