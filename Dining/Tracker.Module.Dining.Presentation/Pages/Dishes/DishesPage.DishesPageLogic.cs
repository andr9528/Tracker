using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Frontend.Core;

namespace Tracker.Module.Dining.Presentation.Pages.Dishes;

internal sealed partial class DishesPage
{
    internal sealed class DishesPageLogic(DishesPageViewModel viewModel) : BaseLogic<DishesPageViewModel>(viewModel)
    {
        public void CreateDishClicked(object sender, RoutedEventArgs e)
        {
            DishCreationPage.DishCreationPageArguments arguments =
                ViewModel.Arguments.ArgumentsFactory.CreateDishCreationPageArguments();

            var page = new DishCreationPage(arguments);

            ViewModel.Arguments.NavigationService.NavigateTo(page, nameof(DishCreationPage));
        }

        public void ShowDetailsClicked(object sender, RoutedEventArgs e)
        {
            Dish? selectedDish = ViewModel.DishesGrid.Api.SelectedDish;

            if (selectedDish == null) return;

            DishDetailsPage.DishDetailsPageArguments arguments =
                ViewModel.Arguments.ArgumentsFactory.CreateDishDetailsPageArguments(selectedDish.Id);

            var page = new DishDetailsPage(arguments);

            ViewModel.Arguments.NavigationService.NavigateTo(page, nameof(DishDetailsPage));
        }
    }
}
