using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Abstraction.Services;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Presentation.Pages;
using Tracker.Module.Dining.Presentation.Pages.Ingredients;
using Tracker.Module.Dining.Presentation.Pages.Search;
using Tracker.Module.Dining.Presentation.Pages.Templates;
using Tracker.Module.Dining.Presentation.Pieces.Dinners;
using Tracker.Module.Dining.Presentation.Pieces.Ingredients;
using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Pieces;

namespace Tracker.Module.Dining.Presentation;

public sealed class DiningArgumentsFactory(
    IEntityQueryService<Ingredient, SearchableIngredient> ingredientQueryService,
    IEntityQueryService<DinnerTemplate, SearchableDinnerTemplate> dinnerTemplateQueryService,
    IUiDispatcher uiDispatcher,
    ILoggerFactory loggerFactory,
    INavigationService navigationService,
    IDiningImportService diningImportService,
    IStatisticsService statisticsService,
    IMainWindowAccessor accessor) : BaseArgumentsFactory
{
    internal IngredientsGrid.IngredientsGridArguments CreateIngredientsGridArguments(
        GridEntitySource entitySource = GridEntitySource.ALL, int selectedIngredientId = 0)
    {
        return new IngredientsGrid.IngredientsGridArguments(ingredientQueryService, statisticsService, uiDispatcher,
            loggerFactory, this, entitySource, selectedIngredientId);
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

    internal IngredientsPage.IngredientsPageArguments CreateIngredientsPageArguments()
    {
        return new IngredientsPage.IngredientsPageArguments(navigationService, this);
    }

    internal IngredientEditor.IngredientEditorArguments CreateIngredientEditorArguments(
        Ingredient? ingredient = null, bool isReadOnly = true)
    {
        return new IngredientEditor.IngredientEditorArguments(ingredient, isReadOnly);
    }

    internal IngredientDetailsPage.IngredientDetailsPageArguments CreateIngredientDetailsPageArguments(int ingredientId)
    {
        return new IngredientDetailsPage.IngredientDetailsPageArguments(ingredientId, ingredientQueryService,
            uiDispatcher, loggerFactory, navigationService, this);
    }

    internal IngredientCreationPage.IngredientCreationPageArguments CreateIngredientCreationPageArguments()
    {
        return new IngredientCreationPage.IngredientCreationPageArguments(ingredientQueryService, navigationService,
            loggerFactory, this);
    }

    internal DinnerTemplatesGrid.DinnerTemplatesGridArguments CreateDinnerTemplatesGridArguments(
        GridDisplayMode displayMode = GridDisplayMode.NORMAL, int selectedDinnerTemplateId = 0)
    {
        return new DinnerTemplatesGrid.DinnerTemplatesGridArguments(dinnerTemplateQueryService, uiDispatcher,
            loggerFactory, this, displayMode, selectedDinnerTemplateId);
    }

    internal DinnerTemplatesPage.DinnerTemplatesPageArguments CreateDinnerTemplatesPageArguments()
    {
        return new DinnerTemplatesPage.DinnerTemplatesPageArguments(navigationService, this);
    }

    internal DinnerTemplateCreationPage.DinnerTemplateCreationPageArguments CreateDinnerTemplateCreationPageArguments()
    {
        return new DinnerTemplateCreationPage.DinnerTemplateCreationPageArguments(dinnerTemplateQueryService,
            navigationService, loggerFactory, this);
    }

    internal DinnerTemplateDetailsPage.DinnerTemplateDetailsPageArguments CreateDinnerTemplateDetailsPageArguments(
        int dinnerTemplateId)
    {
        return new DinnerTemplateDetailsPage.DinnerTemplateDetailsPageArguments(dinnerTemplateId,
            dinnerTemplateQueryService, uiDispatcher, loggerFactory, navigationService, this);
    }

    internal DinnerTemplateAdvancedSearchPage.DinnerTemplateAdvancedSearchPageArguments
        CreateDinnerTemplateAdvancedSearchPageArguments(ComplexSearchableDinnerTemplate searchable)
    {
        return new DinnerTemplateAdvancedSearchPage.DinnerTemplateAdvancedSearchPageArguments(searchable,
            navigationService, loggerFactory, this);
    }
}
