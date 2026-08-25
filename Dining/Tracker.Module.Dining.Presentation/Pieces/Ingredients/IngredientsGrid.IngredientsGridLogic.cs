using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Presentation.Pages.Search;
using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Extensions;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients;

internal sealed partial class IngredientsGrid
{
    internal sealed class IngredientsGridLogic : BaseLogic<IngredientsGridViewModel>, IIngredientsGridApi
    {
        private readonly IEntityQueryService<Ingredient, SearchableIngredient> queryService;
        private readonly IUiDispatcher uiDispatcher;
        private readonly ILogger<IngredientsGridLogic> logger;

        public IngredientsGridLogic(IngredientsGridViewModel viewModel) : base(viewModel)
        {
            queryService = ViewModel.Arguments.QueryService;
            uiDispatcher = ViewModel.Arguments.UiDispatcher;
            logger = ViewModel.Arguments.LoggerFactory.CreateLogger<IngredientsGridLogic>();

            ViewModel.SearchChanged += SearchChanged;
        }

        private async void SearchChanged(object? sender, EventArgs e)
        {
            try
            {
                await RefreshIngredients();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Exception caught during refresh of Ingredients");
            }
        }

        internal void AdvancedSearchClicked(object? sender, EventArgs e)
        {
            IngredientAdvancedSearchPage.IngredientAdvancedSearchPageArguments arguments =
                ViewModel.Arguments.ArgumentsFactory.CreateIngredientAdvancedSearchPageArguments(ViewModel.Searchable);

            var page = new IngredientAdvancedSearchPage(arguments);

            arguments.NavigationService.NavigateTo(page, nameof(IngredientAdvancedSearchPage));
        }

        public async Task RefreshIngredients()
        {
            RememberSelectedIngredient();

            List<Ingredient> ingredients = (await queryService.GetEntitiesComplex(ViewModel.Searchable)).ToList();

            if (ViewModel.Arguments.EntitySource == GridEntitySource.SUPPLIED)
            {
                var suppliedIds = suppliedIngredients.Select(x => x.Id).ToHashSet();

                ingredients = ingredients.Where(x => suppliedIds.Contains(x.Id)).ToList();
            }

            ingredients = ViewModel.DataGrid.ApplyCurrentSort(ingredients).ToList();

            logger.LogInformation(
                "Ingredient search returned {IngredientCount} ingredients. Fuzzy search: {UseFuzzySearch}",
                ingredients.Count, string.IsNullOrWhiteSpace(ViewModel.Searchable.Searchable.Name));

            uiDispatcher.TryEnqueue(() =>
            {
                logger.LogDebug("Updating Ingredients collection. Existing count: {ExistingCount}",
                    ViewModel.Ingredients.Count);

                ViewModel.Ingredients.ReplaceItems(ingredients);
                ViewModel.DataGrid.Refresh();

                RestoreSelectedIngredient();

                logger.LogDebug(
                    "Ingredients collection updated. New count: {NewCount}, SelectedIngredientId: {SelectedIngredientId}, SelectedIngredient: '{SelectedIngredientName}'",
                    ViewModel.Ingredients.Count, ViewModel.SelectedIngredientId, ViewModel.SelectedIngredient?.Name);
            });
        }

        private void RememberSelectedIngredient()
        {
            if (ViewModel.SelectedIngredient is null)
                return;

            ViewModel.SelectedIngredientId = ViewModel.SelectedIngredient.Id;
        }

        private void RestoreSelectedIngredient()
        {
            ViewModel.SelectedIngredient =
                ViewModel.Ingredients.FirstOrDefault(x => x.Id == ViewModel.SelectedIngredientId);
        }


        #region Implementation of IIngredientsGridApi

        /// <inheritdoc />
        public Ingredient? SelectedIngredient => ViewModel.SelectedIngredient;

        private readonly List<Ingredient> suppliedIngredients = [];

        /// <inheritdoc />
        public void SetSuppliedIngredients(IEnumerable<Ingredient> ingredients)
        {
            EnsureSuppliedSource();
            ArgumentNullException.ThrowIfNull(ingredients);

            suppliedIngredients.Clear();
            AddUniqueIngredients(ingredients);

            _ = RefreshIngredients();
        }

        /// <inheritdoc />
        public void AddSuppliedIngredients(IEnumerable<Ingredient> ingredients)
        {
            EnsureSuppliedSource();
            ArgumentNullException.ThrowIfNull(ingredients);

            AddUniqueIngredients(ingredients);

            _ = RefreshIngredients();
        }

        /// <inheritdoc />
        public void AddSuppliedIngredient(Ingredient ingredient)
        {
            EnsureSuppliedSource();
            ArgumentNullException.ThrowIfNull(ingredient);

            if (suppliedIngredients.All(x => x.Id != ingredient.Id))
                suppliedIngredients.Add(ingredient);

            _ = RefreshIngredients();
        }

        /// <inheritdoc />
        public void RemoveSuppliedIngredient(Ingredient ingredient)
        {
            EnsureSuppliedSource();
            ArgumentNullException.ThrowIfNull(ingredient);

            suppliedIngredients.RemoveAll(x => x.Id == ingredient.Id);

            _ = RefreshIngredients();
        }

        /// <inheritdoc />
        public void ClearSuppliedIngredients()
        {
            EnsureSuppliedSource();

            suppliedIngredients.Clear();

            _ = RefreshIngredients();
        }

        /// <inheritdoc />
        public Task Refresh()
        {
            return RefreshIngredients();
        }

        private void AddUniqueIngredients(IEnumerable<Ingredient> ingredients)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                if (suppliedIngredients.All(x => x.Id != ingredient.Id))
                    suppliedIngredients.Add(ingredient);
            }
        }

        private void EnsureSuppliedSource()
        {
            if (ViewModel.Arguments.EntitySource != GridEntitySource.SUPPLIED)
                throw new InvalidOperationException(
                    "Supplied ingredients can only be modified when the grid entity source is SUPPLIED.");
        }

        #endregion
    }
}
