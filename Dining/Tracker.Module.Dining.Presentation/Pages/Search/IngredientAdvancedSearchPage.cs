using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Shared.Frontend.Abstraction;

namespace Tracker.Module.Dining.Presentation.Pages.Search;

internal sealed partial class IngredientAdvancedSearchPage : Page
{
    internal IngredientAdvancedSearchPageViewModel ViewModel =>
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
