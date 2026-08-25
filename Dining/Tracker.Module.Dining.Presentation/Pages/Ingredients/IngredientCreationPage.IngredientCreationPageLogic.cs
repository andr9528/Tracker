using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Presentation.Pieces.Ingredients;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Core;

namespace Tracker.Module.Dining.Presentation.Pages.Ingredients
{
    internal sealed partial class IngredientCreationPage
    {
        private sealed class IngredientCreationPageLogic : BaseLogic<IngredientCreationPageViewModel>
        {
            private readonly IEntityQueryService<Ingredient, SearchableIngredient> queryService;
            private readonly INavigationService navigationService;
            private readonly ILogger<IngredientCreationPageLogic> logger;

            public IngredientCreationPageLogic(IngredientCreationPageViewModel viewModel) : base(viewModel)
            {
                queryService = ViewModel.Arguments.IngredientQueryService;
                navigationService = ViewModel.Arguments.NavigationService;
                logger = ViewModel.Arguments.LoggerFactory.CreateLogger<IngredientCreationPageLogic>();
            }

            internal async Task SaveClicked(object sender, RoutedEventArgs e)
            {
                try
                {
                    if (!IsUserInputValid())
                    {
                        logger.LogInformation("Ingredient creation was blocked by invalid input.");

                        return;
                    }

                    Ingredient ingredient = BuildNewIngredient();

                    await queryService.AddEntity(ingredient);

                    logger.LogInformation("Created ingredient {IngredientId} with name '{IngredientName}'.",
                        ingredient.Id, ingredient.Name);

                    IngredientDetailsPage.IngredientDetailsPageArguments arguments =
                        ViewModel.Arguments.ArgumentsFactory.CreateIngredientDetailsPageArguments(ingredient.Id);

                    var details = new IngredientDetailsPage(arguments);

                    navigationService.NavigateTo(details, nameof(IngredientDetailsPage));
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "Caught exception while trying to create a new Ingredient.");
                }
            }

            private bool IsUserInputValid()
            {
                return !string.IsNullOrWhiteSpace(ViewModel.IngredientEditor.ViewModel.Name);
            }

            private Ingredient BuildNewIngredient()
            {
                IngredientEditor.IngredientEditorViewModel editor = ViewModel.IngredientEditor.ViewModel;

                return new Ingredient
                {
                    Name = editor.Name,
                    InStock = editor.InStock,
                };
            }

            internal void CancelClicked(object sender, RoutedEventArgs e)
            {
                navigationService.NavigateBack();
            }
        }
    }
}