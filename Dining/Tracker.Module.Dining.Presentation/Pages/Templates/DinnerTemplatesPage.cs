using Tracker.Shared.Frontend.Abstraction;

namespace Tracker.Module.Dining.Presentation.Pages.Templates;

internal sealed partial class DinnerTemplatesPage : Page, INavigationRefreshable
{
    private DinnerTemplatesPageViewModel ViewModel =>
        (DinnerTemplatesPageViewModel) DataContext;

    public DinnerTemplatesPage(DinnerTemplatesPageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);

        DataContext = new DinnerTemplatesPageViewModel(arguments);

        var logic = new DinnerTemplatesPageLogic(ViewModel);
        var ui = new DinnerTemplatesPageUi(logic, ViewModel);

        Content = ui.CreateContentGrid();
    }

    #region Implementation of INavigationRefreshable

    /// <inheritdoc />
    public void RefreshAfterNavigation()
    {
        ViewModel.DinnerTemplatesGrid.RefreshAfterNavigation();
    }

    #endregion

    internal record DinnerTemplatesPageArguments(
        INavigationService NavigationService,
        DiningArgumentsFactory ArgumentsFactory);
}
