using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Dishes;

internal sealed partial class DishesPage : Border, INavigationRefreshable
{
    private DishesPageViewModel ViewModel =>
        (DishesPageViewModel) DataContext;

    public DishesPage(DishesPageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePageBorder();

        DataContext = new DishesPageViewModel(arguments);

        var logic = new DishesPageLogic(ViewModel);
        var ui = new DishesPageUi(logic, ViewModel);

        Child = ui.CreateContentGrid();
    }

    /// <inheritdoc />
    public void RefreshAfterNavigation()
    {
        ViewModel.DishesGrid.RefreshAfterNavigation();
    }

    internal record DishesPageArguments(INavigationService NavigationService, DiningArgumentsFactory ArgumentsFactory);
}
