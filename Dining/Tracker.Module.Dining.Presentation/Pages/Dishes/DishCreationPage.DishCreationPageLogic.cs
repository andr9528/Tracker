using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Presentation.Pieces.Dishes;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Core;

namespace Tracker.Module.Dining.Presentation.Pages.Dishes;

internal sealed partial class DishCreationPage
{
    private sealed class DishCreationPageLogic : BaseLogic<DishCreationPageViewModel>
    {
        private readonly IEntityQueryService<Dish, SearchableDish> queryService;
        private readonly INavigationService navigationService;
        private readonly ILogger<DishCreationPageLogic> logger;

        public DishCreationPageLogic(DishCreationPageViewModel viewModel) : base(viewModel)
        {
            queryService = ViewModel.Arguments.DishQueryService;
            navigationService = ViewModel.Arguments.NavigationService;
            logger = ViewModel.Arguments.LoggerFactory.CreateLogger<DishCreationPageLogic>();
        }

        internal async Task SaveClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsUserInputValid())
                {
                    logger.LogInformation("Dish creation was blocked by invalid input.");

                    return;
                }

                Dish dish = BuildNewDish();

                await queryService.AddEntity(dish);

                logger.LogInformation("Created dish {DishId} with name '{DishName}'.", dish.Id, dish.Name);

                DishDetailsPage.DishDetailsPageArguments arguments =
                    ViewModel.Arguments.ArgumentsFactory.CreateDishDetailsPageArguments(dish.Id);

                var details = new DishDetailsPage(arguments);

                navigationService.NavigateTo(details, nameof(DishDetailsPage));
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Caught exception while trying to create a new Dish.");
            }
        }

        private bool IsUserInputValid()
        {
            return !string.IsNullOrWhiteSpace(ViewModel.DishEditor.Api.Name);
        }

        private Dish BuildNewDish()
        {
            DishEditor.IDishEditorApi editor = ViewModel.DishEditor.Api;

            var dish = new Dish
            {
                Name = editor.Name,
                DishIngredients = [],
            };

            foreach (Ingredient ingredient in editor.Ingredients)
            {
                dish.DishIngredients.Add(new DishIngredient
                {
                    Dish = dish,
                    Ingredient = ingredient,
                    IngredientId = ingredient.Id,
                });
            }

            return dish;
        }

        internal void CancelClicked(object sender, RoutedEventArgs e)
        {
            navigationService.NavigateBack();
        }
    }
}
