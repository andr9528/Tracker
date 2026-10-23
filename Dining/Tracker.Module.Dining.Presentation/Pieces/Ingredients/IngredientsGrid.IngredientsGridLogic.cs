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

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients;

internal sealed partial class IngredientsGrid
{
    internal sealed class IngredientsGridLogic : BaseLogic<IngredientsGridViewModel>, IIngredientsGridApi
    {
        private readonly ILogger<IngredientsGridLogic> logger;
        private readonly IEntityQueryService<Ingredient, SearchableIngredient> queryService;
        private readonly IStatisticsService statisticsService;

        public IngredientsGridLogic(IngredientsGridViewModel viewModel) : base(viewModel)
        {
            queryService = ViewModel.Arguments.QueryService;
            statisticsService = ViewModel.Arguments.StatisticsService;
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

        public Task RefreshIngredients()
        {
            return ViewModel.EntityDataGrid.Api.Refresh();
        }

        internal async Task<IEnumerable<IngredientGridItem>> GetItems()
        {
            var ingredients = (await queryService.GetEntitiesComplex(ViewModel.Searchable)).ToList();
            ingredients = ApplyEntitySource(ingredients);
            var averageDays = await statisticsService.GetIngredientAverageDaysBetweenUsage();
            var items = ingredients.Select(x => new IngredientGridItem(x, averageDays.GetValueOrDefault(x.Id)))
                .ToList();
            logger.LogInformation("Ingredients search returned {Count} items.", items.Count);
            return items;
        }

        private List<Ingredient> ApplyEntitySource(List<Ingredient> ingredients)
        {
            if (ViewModel.Arguments.EntitySource == GridEntitySource.ALL)
            {
                return ingredients;
            }

            var suppliedIds = suppliedIngredients.Select(x => x.Id).ToHashSet();

            return ViewModel.Arguments.EntitySource switch
            {
                GridEntitySource.SUPPLIED => ingredients.Where(x => suppliedIds.Contains(x.Id)).ToList(),

                GridEntitySource.NOT_SUPPLIED => ingredients.Where(x => !suppliedIds.Contains(x.Id)).ToList(),

                var _ => ingredients,
            };
        }


        #region Implementation of IIngredientsGridApi

        /// <inheritdoc />
        public Ingredient? SelectedIngredient => ViewModel.SelectedIngredient;

        private readonly List<Ingredient> suppliedIngredients = [];

        /// <inheritdoc />
        public IReadOnlyCollection<Ingredient> SuppliedIngredients =>
            suppliedIngredients.AsReadOnly();

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
            {
                suppliedIngredients.Add(ingredient);
            }

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
                {
                    suppliedIngredients.Add(ingredient);
                }
            }
        }

        private void EnsureSuppliedSource()
        {
            if (ViewModel.Arguments.EntitySource == GridEntitySource.ALL)
            {
                throw new InvalidOperationException(
                    "Supplied ingredients can only be modified when the grid entity source is SUPPLIED or NOT_SUPPLIED.");
            }
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
