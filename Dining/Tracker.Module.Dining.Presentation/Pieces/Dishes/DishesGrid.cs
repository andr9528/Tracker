using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Abstraction.Interfaces.Frontend;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Dishes;

internal sealed partial class DishesGrid : Border, INavigationRefreshable, ISuppliedEntityGrid<Dish>
{
    private DishesGridViewModel ViewModel =>
        (DishesGridViewModel) DataContext;

    private DishesGridLogic Logic { get; }

    public IDishesGridApi Api => Logic;

    public DishesGrid(DishesGridArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePieceBorder();

        DataContext = new DishesGridViewModel(arguments);

        Logic = new DishesGridLogic(ViewModel);
        var ui = new DishesGridUi(Logic, ViewModel);

        Child = ui.CreateContentGrid();

        _ = Logic.RefreshDishes();
    }

    /// <inheritdoc />
    public void RefreshAfterNavigation()
    {
        var logger = ViewModel.Arguments.LoggerFactory.CreateLogger<DishesGrid>();

        logger.LogInformation("Refreshing Dishes after Navigation");

        _ = Api.Refresh();
    }

    internal record DishesGridArguments(
        IEntityQueryService<Dish, SearchableDish> QueryService,
        IUiDispatcher UiDispatcher,
        ILoggerFactory LoggerFactory,
        DiningArgumentsFactory ArgumentsFactory,
        GridEntitySource EntitySource = GridEntitySource.ALL,
        GridDisplayMode DisplayMode = GridDisplayMode.NORMAL,
        int SelectedDishId = 0);

    internal interface IDishesGridApi
    {
        Dish? SelectedDish { get; }

        IReadOnlyCollection<Dish> SuppliedDishes { get; }

        void SetSuppliedDishes(IEnumerable<Dish> dishes);
        void AddSuppliedDishes(IEnumerable<Dish> dishes);
        void AddSuppliedDish(Dish dish);
        void RemoveSuppliedDish(Dish dish);
        void ClearSuppliedDishes();

        Task Refresh();
    }

    #region Implementation of IEntitySelectionGrid<Dish>

    /// <inheritdoc />
    public Dish? SelectedEntity => Api.SelectedDish;

    /// <inheritdoc />
    public UIElement Content => this;

    #endregion

    #region Implementation of ISuppliedEntityGrid<Dish>

    /// <inheritdoc />
    public void AddSuppliedEntity(Dish entity)
    {
        Api.AddSuppliedDish(entity);
    }

    /// <inheritdoc />
    public void RemoveSuppliedEntity(Dish entity)
    {
        Api.RemoveSuppliedDish(entity);
    }

    #endregion
}
