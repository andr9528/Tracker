using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Extensions;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Dishes;

internal sealed partial class DishesGrid
{
    internal sealed class DishesGridLogic : BaseLogic<DishesGridViewModel>, IDishesGridApi
    {
        private readonly ILogger<DishesGridLogic> logger;
        private readonly IEntityQueryService<Dish, SearchableDish> queryService;

        private readonly List<Dish> suppliedDishes = [];

        public DishesGridLogic(DishesGridViewModel viewModel) : base(viewModel)
        {
            queryService = ViewModel.Arguments.QueryService;
            logger = ViewModel.Arguments.LoggerFactory.CreateLogger<DishesGridLogic>();

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
            dishes = ApplyEntitySource(dishes);
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);
            var items = dishes.Select(x => new DishGridItem(x, currentDate)).ToList();
            logger.LogInformation("Dishes search returned {Count} items.", items.Count);
            return items;
        }

        private List<Dish> ApplyEntitySource(List<Dish> dishes)
        {
            if (ViewModel.Arguments.EntitySource == GridEntitySource.ALL)
            {
                return dishes;
            }

            var suppliedIds = suppliedDishes.Select(x => x.Id).ToHashSet();

            return ViewModel.Arguments.EntitySource switch
            {
                GridEntitySource.SUPPLIED => dishes.Where(x => suppliedIds.Contains(x.Id)).ToList(),

                GridEntitySource.NOT_SUPPLIED => dishes.Where(x => !suppliedIds.Contains(x.Id)).ToList(),

                var _ => dishes,
            };
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
            suppliedDishes.AsReadOnly();

        /// <inheritdoc />
        public void SetSuppliedDishes(IEnumerable<Dish> dishes)
        {
            EnsureSuppliedSource();
            ArgumentNullException.ThrowIfNull(dishes);

            suppliedDishes.Clear();
            AddUniqueDishes(dishes);

            _ = RefreshDishes();
        }

        /// <inheritdoc />
        public void AddSuppliedDishes(IEnumerable<Dish> dishes)
        {
            EnsureSuppliedSource();
            ArgumentNullException.ThrowIfNull(dishes);

            AddUniqueDishes(dishes);

            _ = RefreshDishes();
        }

        /// <inheritdoc />
        public void AddSuppliedDish(Dish dish)
        {
            EnsureSuppliedSource();
            ArgumentNullException.ThrowIfNull(dish);

            if (suppliedDishes.All(x => x.Id != dish.Id))
            {
                suppliedDishes.Add(dish);
            }

            _ = RefreshDishes();
        }

        /// <inheritdoc />
        public void RemoveSuppliedDish(Dish dish)
        {
            EnsureSuppliedSource();
            ArgumentNullException.ThrowIfNull(dish);

            suppliedDishes.RemoveAll(x => x.Id == dish.Id);

            _ = RefreshDishes();
        }

        /// <inheritdoc />
        public void ClearSuppliedDishes()
        {
            EnsureSuppliedSource();

            suppliedDishes.Clear();

            _ = RefreshDishes();
        }

        /// <inheritdoc />
        public Task Refresh()
        {
            return RefreshDishes();
        }

        private void AddUniqueDishes(IEnumerable<Dish> dishes)
        {
            foreach (Dish dish in dishes)
            {
                if (suppliedDishes.All(x => x.Id != dish.Id))
                {
                    suppliedDishes.Add(dish);
                }
            }
        }

        private void EnsureSuppliedSource()
        {
            if (ViewModel.Arguments.EntitySource == GridEntitySource.ALL)
            {
                throw new InvalidOperationException(
                    "Supplied dishes can only be modified when the grid entity source is SUPPLIED or NOT_SUPPLIED.");
            }
        }

        #endregion
    }
}
