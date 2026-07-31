using Tracker.Shared.Frontend.Abstraction;

namespace Tracker.Module.Dining.Presentation.Pages;

internal sealed partial class DiningHomepage : Page
{
    private DiningHomepageViewModel ViewModel =>
        (DiningHomepageViewModel) DataContext;

    public DiningHomepage(DiningHomepageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);

        DataContext = new DiningHomepageViewModel(arguments);

        var logic = new DiningHomepageLogic(ViewModel);
        var ui = new DiningHomepageUi(logic, ViewModel);

        Content = ui.CreateContentGrid();
    }

    internal record DiningHomepageArguments(
        INavigationService NavigationService,
        DiningArgumentsFactory ArgumentsFactory)
    {
    }
}
