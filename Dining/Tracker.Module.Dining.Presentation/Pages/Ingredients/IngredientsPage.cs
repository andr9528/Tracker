using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Ingredients;

internal sealed partial class IngredientsPage : Border, INavigationRefreshable
{
    public IngredientsPage(IngredientsPageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePageBorder();

        DataContext = new IngredientsPageViewModel(arguments);

        var logic = new IngredientsPageLogic(ViewModel);
        var ui = new IngredientsPageUi(logic, ViewModel);

        Child = ui.CreateContentGrid();
    }

    private IngredientsPageViewModel ViewModel =>
        (IngredientsPageViewModel) DataContext;

    /// <inheritdoc />
    public void RefreshAfterNavigation()
    {
        ViewModel.IngredientsGrid.RefreshAfterNavigation();
    }

    internal record IngredientsPageArguments(
        INavigationService NavigationService,
        DiningArgumentsFactory ArgumentsFactory);
}
