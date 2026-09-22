using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Abstraction.Services;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Abstraction.Interfaces.Frontend;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients;

internal sealed partial class IngredientsGrid : Border, INavigationRefreshable, ISuppliedEntityGrid<Ingredient>
{
    private IngredientsGridViewModel ViewModel => (IngredientsGridViewModel) DataContext;
    private IngredientsGridLogic Logic { get; }
    public IIngredientsGridApi Api => Logic;

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

        _ = Api.Refresh();
    }

    internal record IngredientsGridArguments(
        IEntityQueryService<Ingredient, SearchableIngredient> QueryService,
        IStatisticsService StatisticsService,
        IUiDispatcher UiDispatcher,
        ILoggerFactory LoggerFactory,
        DiningArgumentsFactory ArgumentsFactory,
        GridEntitySource EntitySource = GridEntitySource.ALL,
        GridDisplayMode DisplayMode = GridDisplayMode.NORMAL,
        int SelectedIngredientId = 0);

    internal interface IIngredientsGridApi
    {
        Ingredient? SelectedIngredient { get; }

        void SetSuppliedIngredients(IEnumerable<Ingredient> ingredients);
        void AddSuppliedIngredients(IEnumerable<Ingredient> ingredients);
        void AddSuppliedIngredient(Ingredient ingredient);
        void RemoveSuppliedIngredient(Ingredient ingredient);
        void ClearSuppliedIngredients();

        Task Refresh();
    }

    #region Implementation of IEntitySelectionGrid<Ingredient>

    /// <inheritdoc />
    public Ingredient? SelectedEntity => Api.SelectedIngredient;

    /// <inheritdoc />
    public UIElement Content => this;

    #endregion

    #region Implementation of ISuppliedEntityGrid<Ingredient>

    /// <inheritdoc />
    public void AddSuppliedEntity(Ingredient entity)
    {
        Api.AddSuppliedIngredient(entity);
    }

    /// <inheritdoc />
    public void RemoveSuppliedEntity(Ingredient entity)
    {
        Api.RemoveSuppliedIngredient(entity);
    }

    #endregion
}
