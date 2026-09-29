using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Abstraction.Entity;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Presentation.Pieces.Dishes;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Core.Details;

namespace Tracker.Module.Dining.Presentation.Pages.Dishes;

internal sealed partial class DishDetailsPage
{
    internal sealed class DishDetailsPageLogic : BaseDetailsPageLogic<DishDetailsPageViewModel>
    {
        private readonly IEntityQueryService<Dish, SearchableDish> queryService;
        private readonly IUiDispatcher uiDispatcher;
        private readonly ILogger<DishDetailsPageLogic> logger;

        public DishDetailsPageLogic(DishDetailsPageViewModel viewModel) : base(viewModel)
        {
            queryService = ViewModel.Arguments.DishQueryService;
            uiDispatcher = ViewModel.Arguments.UiDispatcher;
            logger = ViewModel.Arguments.LoggerFactory.CreateLogger<DishDetailsPageLogic>();
        }

        internal async Task RefreshDish()
        {
            Dish? dish = await queryService.GetEntity(CreateSearchableDish());

            if (dish is null)
            {
                return;
            }

            uiDispatcher.TryEnqueue(() =>
            {
                ViewModel.Dish = dish;

                ApplyDishToEditor();
                UpdateHasChanges();
            });
        }

        private SearchableDish CreateSearchableDish()
        {
            return new SearchableDish
            {
                Id = ViewModel.Arguments.DishId,
            };
        }

        internal void RegisterDishEditorEvents()
        {
            ViewModel.DishEditor.Api.NameChanged += DishEditorChanged;
            ViewModel.DishEditor.Api.IngredientsChanged += DishEditorChanged;
        }

        private void DishEditorChanged(object? sender, EventArgs e)
        {
            UpdateHasChanges();
        }

        private void ApplyEditorValuesToDish()
        {
            DishEditor.IDishEditorApi editor = ViewModel.DishEditor.Api;

            logger.LogDebug("Applying dish changes. DishId={DishId}", ViewModel.Dish.Id);

            logger.LogDebug("Name: '{OldValue}' -> '{NewValue}'", ViewModel.Dish.Name, editor.Name);

            ViewModel.Dish.Name = editor.Name;

            ViewModel.Dish.DishIngredients.Clear();

            foreach (Ingredient ingredient in editor.Ingredients)
            {
                ViewModel.Dish.DishIngredients.Add(new DishIngredient
                {
                    DishId = ViewModel.Dish.Id,
                    IngredientId = ingredient.Id,
                    Dish = ViewModel.Dish,
                    Ingredient = ingredient,
                });
            }
        }

        private void ApplyDishToEditor()
        {
            ViewModel.DishEditor.Api.ApplyDish(ViewModel.Dish);
        }

        public override async Task DeleteClicked(object sender, RoutedEventArgs e)
        {
            ContentDialogResult result = await ShowDeleteConfirmation(
                "Delete dish?", "This will permanently delete the current dish.");

            if (result != ContentDialogResult.Primary)
            {
                return;
            }

            await queryService.DeleteEntityById(ViewModel.Dish.Id);

            logger.LogInformation("Deleted dish {DishId}", ViewModel.Dish.Id);

            NavigateBack();
        }

        protected override void SetEditorReadOnly(bool isReadOnly)
        {
            ViewModel.DishEditor.Api.SetReadOnly(isReadOnly);
        }

        protected override async Task SaveChanges()
        {
            ApplyEditorValuesToDish();

            await queryService.UpdateEntity(ViewModel.Dish);

            logger.LogInformation("Saved changes to dish {DishId}.", ViewModel.Dish.Id);
        }

        protected override void ApplyEntityToEditor()
        {
            ApplyDishToEditor();
        }

        protected override void UpdateHasChanges()
        {
            DishEditor.IDishEditorApi editor = ViewModel.DishEditor.Api;

            ViewModel.HasChanges = editor.Name != ViewModel.Dish.Name ||
                                   !HaveSameIngredients(editor.Ingredients, ViewModel.Dish.DishIngredients);

            UpdateSaveAndCancelText();
        }

        private bool HaveSameIngredients(
            IEnumerable<Ingredient> ingredients, IEnumerable<IDishIngredient> dishIngredients)
        {
            return ingredients.Select(x => x.Id).Order()
                .SequenceEqual(dishIngredients.Select(x => x.IngredientId).Order());
        }

        protected override void NavigateBack()
        {
            ViewModel.Arguments.NavigationService.NavigateBack();
        }

        protected override void LogSaveError(Exception exception)
        {
            logger.LogError(exception, "Failed to save changes to the dish.");
        }
    }
}
