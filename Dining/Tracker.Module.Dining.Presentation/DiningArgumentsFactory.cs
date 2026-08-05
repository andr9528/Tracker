using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Abstraction.Services;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Presentation.Pages;
using Tracker.Module.Dining.Presentation.Pages.Search;
using Tracker.Module.Dining.Presentation.Pieces.Ingredients;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Pieces;

namespace Tracker.Module.Dining.Presentation;

public sealed class DiningArgumentsFactory(
    IEntityQueryService<Ingredient, SearchableIngredient> ingredientQueryService,
    IUiDispatcher uiDispatcher,
    ILoggerFactory loggerFactory,
    INavigationService navigationService,
    IDiningImportService diningImportService,
    IStatisticsService statisticsService,
    IMainWindowAccessor accessor) : BaseArgumentsFactory
{
    internal IngredientsGrid.IngredientsGridArguments CreateIngredientsGridArguments(int selectedIngredientId = 0)
    {
        return new IngredientsGrid.IngredientsGridArguments(ingredientQueryService, uiDispatcher, loggerFactory, this,
            selectedIngredientId);
    }

    internal IngredientAdvancedSearchPage.IngredientAdvancedSearchPageArguments
        CreateIngredientAdvancedSearchPageArguments(ComplexSearchableIngredient searchable)
    {
        return new IngredientAdvancedSearchPage.IngredientAdvancedSearchPageArguments(searchable, navigationService,
            loggerFactory, this);
    }

    internal DiningHomepage.DiningHomepageArguments CreateDiningHomepageArguments()
    {
        return new DiningHomepage.DiningHomepageArguments(navigationService, statisticsService, loggerFactory, this);
    }

    internal DiningImportPage.DiningImportPageArguments CreateDiningImportPageArguments()
    {
        return new DiningImportPage.DiningImportPageArguments(diningImportService, loggerFactory, accessor);
    }
}
