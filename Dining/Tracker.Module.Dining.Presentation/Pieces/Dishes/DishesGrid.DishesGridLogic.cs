using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Extensions;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Factory;
using Tracker.Shared.Services.Managers;

namespace Tracker.Module.Dining.Presentation.Pieces.Dishes;

internal sealed partial class DishesGrid
{
    internal sealed class DishesGridLogic : BaseLogic<DishesGridViewModel>, IDishesGridApi
    {
        private readonly ILogger<DishesGridLogic> logger;
        private readonly IEntityQueryService<Dish, SearchableDish> queryService;

        private readonly SuppliedEntityManager<Dish> suppliedManager;

        public DishesGridLogic(DishesGridViewModel viewModel) : base(viewModel)
        {
            queryService = ViewModel.Arguments.QueryService;
            logger = ViewModel.Arguments.LoggerFactory.CreateLogger<DishesGridLogic>();

            suppliedManager = new SuppliedEntityManager<Dish>(ViewModel.Arguments.EntitySource, x => x.Id);

            ViewModel.SearchChanged += SearchChanged;
        }

        private async void SearchChanged(object? sender, EventArgs e)
        {
            try
            {
                await RefreshDishes();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Exception caught during refresh of Dishes");
            }
        }

        public Task RefreshDishes()
        {
            return ViewModel.EntityDataGrid.Api.Refresh();
        }

        internal async Task<IEnumerable<DishGridItem>> GetItems()
        {
            var dishes = (await queryService.GetEntitiesComplex(ViewModel.Searchable)).ToList();
            dishes = suppliedManager.ApplyEntitySource(dishes);
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);
            var items = dishes.Select(x => new DishGridItem(x, currentDate)).ToList();
            logger.LogInformation("Dishes search returned {Count} items.", items.Count);
            return items;
        }

        #region Implementation of IDishesGridApi

        /// <inheritdoc />
        public Dish? SelectedEntity => ViewModel.SelectedDish;

        /// <inheritdoc />
        public event EventHandler? SelectionChanged
        {
            add => ViewModel.EntityDataGrid.Api.SelectionChanged += value;
            remove => ViewModel.EntityDataGrid.Api.SelectionChanged -= value;
        }

        /// <inheritdoc />
        public IReadOnlyCollection<Dish> SuppliedDishes =>
            suppliedManager.SuppliedEntities;

        /// <inheritdoc />
        public void SetSuppliedDishes(IEnumerable<Dish> dishes)
        {
            suppliedManager.Set(dishes);

            _ = RefreshDishes();
        }

        /// <inheritdoc />
        public void AddSuppliedDishes(IEnumerable<Dish> dishes)
        {
            suppliedManager.AddRange(dishes);

            _ = RefreshDishes();
        }

        /// <inheritdoc />
        public void AddSuppliedDish(Dish dish)
        {
            suppliedManager.Add(dish);

            _ = RefreshDishes();
        }

        /// <inheritdoc />
        public void RemoveSuppliedDish(Dish dish)
        {
            suppliedManager.Remove(dish);

            _ = RefreshDishes();
        }

        /// <inheritdoc />
        public void ClearSuppliedDishes()
        {
            suppliedManager.Clear();

            _ = RefreshDishes();
        }

        /// <inheritdoc />
        public Task Refresh()
        {
            return RefreshDishes();
        }

        #endregion
    }
}
