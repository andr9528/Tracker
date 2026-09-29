using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Abstraction.Attributes;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Search;

[SkipOnBackNavigation]
internal sealed partial class IngredientAdvancedSearchPage : Border
{
    private IngredientAdvancedSearchPageViewModel ViewModel =>
        (IngredientAdvancedSearchPageViewModel) DataContext;

    public IngredientAdvancedSearchPage(IngredientAdvancedSearchPageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePageBorder();

        DataContext = new IngredientAdvancedSearchPageViewModel(arguments);

        var logic = new IngredientAdvancedSearchPageLogic(ViewModel);
        var ui = new IngredientAdvancedSearchPageUi(logic, ViewModel);

        Child = ui.CreateContentGrid();
    }

    internal record IngredientAdvancedSearchPageArguments(
        ComplexSearchableIngredient Searchable,
        INavigationService NavigationService,
        ILoggerFactory LoggerFactory,
        DiningArgumentsFactory ArgumentsFactory)
    {
    }
}
