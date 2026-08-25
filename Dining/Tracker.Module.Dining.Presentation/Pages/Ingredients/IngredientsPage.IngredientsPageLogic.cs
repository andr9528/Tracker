using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Frontend.Core;

namespace Tracker.Module.Dining.Presentation.Pages.Ingredients
{
    internal sealed partial class IngredientsPage
    {
        internal sealed class IngredientsPageLogic(IngredientsPageViewModel viewModel)
            : BaseLogic<IngredientsPageViewModel>(viewModel)
        {
            public void CreateIngredientClicked(object sender, RoutedEventArgs e)
            {
                IngredientCreationPage.IngredientCreationPageArguments arguments =
                    ViewModel.Arguments.ArgumentsFactory.CreateIngredientCreationPageArguments();

                var page = new IngredientCreationPage(arguments);

                ViewModel.Arguments.NavigationService.NavigateTo(page, nameof(IngredientCreationPage));
            }

            public void ShowDetailsClicked(object sender, RoutedEventArgs e)
            {
                Ingredient? selectedIngredient = ViewModel.IngredientsGrid.Api.SelectedIngredient;

                if (selectedIngredient == null) return;

                IngredientDetailsPage.IngredientDetailsPageArguments arguments =
                    ViewModel.Arguments.ArgumentsFactory.CreateIngredientDetailsPageArguments(selectedIngredient.Id);

                var page = new IngredientDetailsPage(arguments);

                ViewModel.Arguments.NavigationService.NavigateTo(page, nameof(IngredientDetailsPage));
            }
        }
    }
}
