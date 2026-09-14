using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Abstraction.Attributes;

namespace Tracker.Module.Dining.Presentation.Pages.Search;

[SkipOnBackNavigation]
internal sealed partial class IngredientAdvancedSearchPage : Page
{
    private IngredientAdvancedSearchPageViewModel ViewModel =>
        (IngredientAdvancedSearchPageViewModel) DataContext;

    public IngredientAdvancedSearchPage(IngredientAdvancedSearchPageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);

        DataContext = new IngredientAdvancedSearchPageViewModel(arguments);

        var logic = new IngredientAdvancedSearchPageLogic(ViewModel);
        var ui = new IngredientAdvancedSearchPageUi(logic, ViewModel);

        Content = ui.CreateContentGrid();
    }

    internal record IngredientAdvancedSearchPageArguments(
        ComplexSearchableIngredient Searchable,
        INavigationService NavigationService,
        ILoggerFactory LoggerFactory,
        DiningArgumentsFactory ArgumentsFactory)
    {
    }
}
