using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Abstraction.Attributes;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Dishes;

/// <summary>
/// Creates a new dish. Editing is always enabled.
/// </summary>
[SkipOnBackNavigation]
internal sealed partial class DishCreationPage : Border
{
    public DishCreationPage(DishCreationPageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePageBorder();

        DataContext = new DishCreationPageViewModel(arguments);

        var viewModel = (DishCreationPageViewModel) DataContext;
        var logic = new DishCreationPageLogic(viewModel);
        var ui = new DishCreationPageUi(logic, viewModel);

        Child = ui.CreateContentGrid();
    }

    internal sealed record DishCreationPageArguments(
        IEntityQueryService<Dish, SearchableDish> DishQueryService,
        INavigationService NavigationService,
        ILoggerFactory LoggerFactory,
        DiningArgumentsFactory ArgumentsFactory);
}
