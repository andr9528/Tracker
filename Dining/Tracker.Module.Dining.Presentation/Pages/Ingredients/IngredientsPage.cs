using Tracker.Shared.Frontend.Abstraction;

namespace Tracker.Module.Dining.Presentation.Pages.Ingredients
{
    internal sealed partial class IngredientsPage : Page, INavigationRefreshable
    {
        private IngredientsPageViewModel ViewModel =>
            (IngredientsPageViewModel) DataContext;

        public IngredientsPage(IngredientsPageArguments arguments)
        {
            ArgumentNullException.ThrowIfNull(arguments);

            DataContext = new IngredientsPageViewModel(arguments);

            var logic = new IngredientsPageLogic(ViewModel);
            var ui = new IngredientsPageUi(logic, ViewModel);

            Content = ui.CreateContentGrid();
        }

        /// <inheritdoc />
        public void RefreshAfterNavigation()
        {
            ViewModel.IngredientsGrid.RefreshAfterNavigation();
        }

        internal record IngredientsPageArguments(
            INavigationService NavigationService,
            DiningArgumentsFactory ArgumentsFactory);
    }
}
