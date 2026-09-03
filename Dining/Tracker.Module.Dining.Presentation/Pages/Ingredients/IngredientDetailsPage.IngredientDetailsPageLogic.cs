using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Presentation.Pieces.Ingredients;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Core.Details;

namespace Tracker.Module.Dining.Presentation.Pages.Ingredients
{
    internal sealed partial class IngredientDetailsPage
    {
        internal sealed class IngredientDetailsPageLogic : BaseDetailsPageLogic<IngredientDetailsPageViewModel>
        {
            private readonly IEntityQueryService<Ingredient, SearchableIngredient> queryService;
            private readonly IUiDispatcher uiDispatcher;
            private readonly ILogger<IngredientDetailsPageLogic> logger;

            public IngredientDetailsPageLogic(IngredientDetailsPageViewModel viewModel) : base(viewModel)
            {
                queryService = ViewModel.Arguments.IngredientQueryService;
                uiDispatcher = ViewModel.Arguments.UiDispatcher;
                logger = ViewModel.Arguments.LoggerFactory.CreateLogger<IngredientDetailsPageLogic>();
            }

            internal async Task RefreshIngredient()
            {
                Ingredient? ingredient = await queryService.GetEntity(CreateSearchableIngredient());

                if (ingredient is null)
                    return;

                uiDispatcher.TryEnqueue(() =>
                {
                    ViewModel.Ingredient = ingredient;

                    ApplyIngredientToEditor();
                    UpdateHasChanges();
                });
            }

            private SearchableIngredient CreateSearchableIngredient()
            {
                return new SearchableIngredient
                {
                    Id = ViewModel.Arguments.IngredientId,
                };
            }

            internal void RegisterIngredientEditorEvents()
            {
                ViewModel.IngredientEditor.Api.NameChanged += IngredientEditorChanged;
                ViewModel.IngredientEditor.Api.InStockChanged += IngredientEditorChanged;
            }

            private void IngredientEditorChanged(object? sender, EventArgs e)
            {
                UpdateHasChanges();
            }

            private void ApplyEditorValuesToIngredient()
            {
                IngredientEditor.IIngredientEditorApi editor = ViewModel.IngredientEditor.Api;

                logger.LogDebug("Applying ingredient changes. IngredientId={IngredientId}", ViewModel.Ingredient.Id);

                logger.LogDebug("Name: '{OldValue}' -> '{NewValue}'", ViewModel.Ingredient.Name, editor.Name);

                logger.LogDebug("InStock: '{OldValue}' -> '{NewValue}'", ViewModel.Ingredient.InStock, editor.InStock);

                ViewModel.Ingredient.Name = editor.Name;
                ViewModel.Ingredient.InStock = editor.InStock;
            }

            private void ApplyIngredientToEditor()
            {
                ViewModel.IngredientEditor.Api.ApplyIngredient(ViewModel.Ingredient);
            }

            public override async Task DeleteClicked(object sender, RoutedEventArgs e)
            {
                ContentDialogResult result = await ShowDeleteConfirmation("Delete ingredient?",
                    "This will permanently delete the current ingredient.");

                if (result != ContentDialogResult.Primary)
                    return;

                await queryService.DeleteEntityById(ViewModel.Ingredient.Id);

                logger.LogInformation("Deleted ingredient {IngredientId}", ViewModel.Ingredient.Id);

                NavigateBack();
            }

            protected override void SetEditorReadOnly(bool isReadOnly)
            {
                ViewModel.IngredientEditor.Api.SetReadOnly(isReadOnly);
            }

            protected override async Task SaveChanges()
            {
                ApplyEditorValuesToIngredient();

                await queryService.UpdateEntity(ViewModel.Ingredient);

                logger.LogInformation("Saved changes to ingredient {IngredientId}.", ViewModel.Ingredient.Id);
            }

            protected override void ApplyEntityToEditor()
            {
                ApplyIngredientToEditor();
            }

            protected override void UpdateHasChanges()
            {
                IngredientEditor.IIngredientEditorApi editor = ViewModel.IngredientEditor.Api;

                ViewModel.HasChanges = editor.Name != ViewModel.Ingredient.Name ||
                                       editor.InStock != ViewModel.Ingredient.InStock;

                UpdateSaveAndCancelText();
            }

            protected override void NavigateBack()
            {
                ViewModel.Arguments.NavigationService.NavigateBack();
            }

            protected override void LogSaveError(Exception exception)
            {
                logger.LogError(exception, "Failed to save changes to the ingredient.");
            }
        }
    }
}
