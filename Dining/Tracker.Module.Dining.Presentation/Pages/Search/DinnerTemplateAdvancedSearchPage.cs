using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Abstraction.Attributes;

namespace Tracker.Module.Dining.Presentation.Pages.Search;

[SkipOnBackNavigation]
internal sealed partial class DinnerTemplateAdvancedSearchPage : Page
{
    private DinnerTemplateAdvancedSearchPageViewModel ViewModel =>
        (DinnerTemplateAdvancedSearchPageViewModel) DataContext;

    public DinnerTemplateAdvancedSearchPage(DinnerTemplateAdvancedSearchPageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);

        DataContext = new DinnerTemplateAdvancedSearchPageViewModel(arguments);

        var logic = new DinnerTemplateAdvancedSearchPageLogic(ViewModel);
        var ui = new DinnerTemplateAdvancedSearchPageUi(logic, ViewModel);

        Content = ui.CreateContentGrid();
    }

    internal record DinnerTemplateAdvancedSearchPageArguments(
        ComplexSearchableDinnerTemplate Searchable,
        INavigationService NavigationService,
        ILoggerFactory LoggerFactory,
        DiningArgumentsFactory ArgumentsFactory);
}
