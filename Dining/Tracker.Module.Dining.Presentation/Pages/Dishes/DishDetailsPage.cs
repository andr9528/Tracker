using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Dishes;

internal sealed partial class DishDetailsPage : Border
{
    public DishDetailsPage(DishDetailsPageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePageBorder();

        DataContext = new DishDetailsPageViewModel(arguments);

        var viewModel = (DishDetailsPageViewModel) DataContext;
        var logic = new DishDetailsPageLogic(viewModel);
        var ui = new DishDetailsPageUi(logic, viewModel);

        Child = ui.CreateContentGrid();

        _ = logic.RefreshDish();
    }

    internal sealed record DishDetailsPageArguments(
        int DishId,
        IEntityQueryService<Dish, SearchableDish> DishQueryService,
        IUiDispatcher UiDispatcher,
        ILoggerFactory LoggerFactory,
        INavigationService NavigationService,
        DiningArgumentsFactory ArgumentsFactory);
}
