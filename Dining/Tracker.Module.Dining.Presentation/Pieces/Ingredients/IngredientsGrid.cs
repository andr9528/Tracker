using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients;

internal sealed partial class IngredientsGrid : Border, INavigationRefreshable
{
    internal IngredientsGridViewModel ViewModel => (IngredientsGridViewModel) DataContext;

    private IngredientsGridLogic Logic { get; }

    public IngredientsGrid(IngredientsGridArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePieceBorder();

        DataContext = new IngredientsGridViewModel(arguments);

        Logic = new IngredientsGridLogic(ViewModel);
        var ui = new IngredientsGridUi(Logic, ViewModel);

        Child = ui.CreateContentGrid();

        _ = Logic.RefreshIngredients();
    }

    /// <inheritdoc />
    public void RefreshAfterNavigation()
    {
        var logger = ViewModel.Arguments.LoggerFactory.CreateLogger<IngredientsGrid>();
        logger.LogInformation("Refreshing Ingredients after Navigation");

        _ = Logic.RefreshIngredients();
    }

    internal record IngredientsGridArguments(
        IEntityQueryService<Ingredient, SearchableIngredient> QueryService,
        IUiDispatcher UiDispatcher,
        ILoggerFactory LoggerFactory,
        DiningArgumentsFactory ArgumentsFactory,
        int SelectedIngredientId = 0);
}
