using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Abstraction.Services;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages;

internal sealed partial class DiningHomepage : Border
{
    private DiningHomepageViewModel ViewModel =>
        (DiningHomepageViewModel) DataContext;

    public DiningHomepage(DiningHomepageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePageBorder();

        DataContext = new DiningHomepageViewModel(arguments);

        var logic = new DiningHomepageLogic(ViewModel);
        var ui = new DiningHomepageUi(logic, ViewModel);

        Child = ui.CreateContentGrid();

        Loaded += logic.PageLoaded;
    }

    internal record DiningHomepageArguments(
        INavigationService NavigationService,
        IStatisticsService StatisticsService,
        ILoggerFactory LoggerFactory,
        DiningArgumentsFactory ArgumentsFactory)
    {
    }
}
