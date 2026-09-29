using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Abstraction.Attributes;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Search;

[SkipOnBackNavigation]
internal sealed partial class DinnerTemplateAdvancedSearchPage : Border
{
    private DinnerTemplateAdvancedSearchPageViewModel ViewModel =>
        (DinnerTemplateAdvancedSearchPageViewModel) DataContext;

    public DinnerTemplateAdvancedSearchPage(DinnerTemplateAdvancedSearchPageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePageBorder();

        DataContext = new DinnerTemplateAdvancedSearchPageViewModel(arguments);

        var logic = new DinnerTemplateAdvancedSearchPageLogic(ViewModel);
        var ui = new DinnerTemplateAdvancedSearchPageUi(logic, ViewModel);

        Child = ui.CreateContentGrid();
    }

    internal record DinnerTemplateAdvancedSearchPageArguments(
        ComplexSearchableDinnerTemplate Searchable,
        INavigationService NavigationService,
        ILoggerFactory LoggerFactory,
        DiningArgumentsFactory ArgumentsFactory);
}
