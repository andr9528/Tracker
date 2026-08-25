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
                //CreateIngredientPage.CreateIngredientPageArguments arguments =
                //    ViewModel.Arguments.ArgumentsFactory.CreateCreateIngredientPageArguments();

                //var page = new CreateIngredientPage(arguments);

                //ViewModel.Arguments.NavigationService.NavigateTo(page, nameof(CreateIngredientPage));
            }

            public void ShowDetailsClicked(object sender, RoutedEventArgs e)
            {
            }
        }
    }
}
