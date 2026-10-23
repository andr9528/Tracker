using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Abstraction.Services;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Presentation.Pages.Search;
using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Extensions;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Factory;
using Tracker.Shared.Services.Managers;

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients;

internal sealed partial class IngredientsGrid
{
    internal sealed class IngredientsGridLogic : BaseLogic<IngredientsGridViewModel>, IIngredientsGridApi
    {
        private readonly ILogger<IngredientsGridLogic> logger;
        private readonly IEntityQueryService<Ingredient, SearchableIngredient> queryService;
        private readonly IStatisticsService statisticsService;
        private readonly SuppliedEntityManager<Ingredient> suppliedManager;

        public IngredientsGridLogic(IngredientsGridViewModel viewModel) : base(viewModel)
        {
            queryService = ViewModel.Arguments.QueryService;
            statisticsService = ViewModel.Arguments.StatisticsService;
            logger = ViewModel.Arguments.LoggerFactory.CreateLogger<IngredientsGridLogic>();

            suppliedManager = new SuppliedEntityManager<Ingredient>(ViewModel.Arguments.EntitySource, x => x.Id);

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

        public Task RefreshIngredients()
        {
            return ViewModel.EntityDataGrid.Api.Refresh();
        }

        internal async Task<IEnumerable<IngredientGridItem>> GetItems()
        {
            var ingredients = (await queryService.GetEntitiesComplex(ViewModel.Searchable)).ToList();
            ingredients = suppliedManager.ApplyEntitySource(ingredients);
            var averageDays = await statisticsService.GetIngredientAverageDaysBetweenUsage();
            var items = ingredients.Select(x => new IngredientGridItem(x, averageDays.GetValueOrDefault(x.Id)))
                .ToList();
            logger.LogInformation("Ingredients search returned {Count} items.", items.Count);
            return items;
        }

        #region Implementation of IIngredientsGridApi

        /// <inheritdoc />
        public Ingredient? SelectedIngredient => ViewModel.SelectedIngredient;

        /// <inheritdoc />
        public IReadOnlyCollection<Ingredient> SuppliedIngredients =>
            suppliedManager.SuppliedEntities;

        /// <inheritdoc />
        public void SetSuppliedIngredients(IEnumerable<Ingredient> ingredients)
        {
            suppliedManager.Set(ingredients);

            _ = RefreshIngredients();
        }

        /// <inheritdoc />
        public void AddSuppliedIngredients(IEnumerable<Ingredient> ingredients)
        {
            suppliedManager.AddRange(ingredients);

            _ = RefreshIngredients();
        }

        /// <inheritdoc />
        public void AddSuppliedIngredient(Ingredient ingredient)
        {
            suppliedManager.Add(ingredient);

            _ = RefreshIngredients();
        }

        /// <inheritdoc />
        public void RemoveSuppliedIngredient(Ingredient ingredient)
        {
            suppliedManager.Remove(ingredient);

            _ = RefreshIngredients();
        }

        /// <inheritdoc />
        public void ClearSuppliedIngredients()
        {
            suppliedManager.Clear();

            _ = RefreshIngredients();
        }

        /// <inheritdoc />
        public Task Refresh()
        {
            return RefreshIngredients();
        }

        #endregion

        #region Implementation of IEntitySelectionGridApi<Ingredient>

        /// <inheritdoc />
        public Ingredient? SelectedEntity => ViewModel.SelectedIngredient;

        /// <inheritdoc />
        public event EventHandler? SelectionChanged
        {
            add => ViewModel.EntityDataGrid.Api.SelectionChanged += value;
            remove => ViewModel.EntityDataGrid.Api.SelectionChanged -= value;
        }

        #endregion
    }
}
