using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Frontend.Core;

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients
{
    internal sealed partial class IngredientEditor
    {
        internal sealed class IngredientEditorLogic : BaseLogic<IngredientEditorViewModel>, IIngredientEditorApi
        {
            public IngredientEditorLogic(IngredientEditorViewModel viewModel) : base(viewModel)
            {
                ViewModel.IsReadOnlyChanged += OnIsReadOnlyChanged;
            }

            #region Implementation of IIngredientEditorApi

            /// <inheritdoc />
            public string Name => ViewModel.Name;

            /// <inheritdoc />
            public bool InStock => ViewModel.InStock;

            /// <inheritdoc />
            public IReadOnlyCollection<Dish> Dishes =>
                ViewModel.SuppliedDishesGrid.Api.SuppliedDishes;

            /// <inheritdoc />
            public event EventHandler? NameChanged
            {
                add => ViewModel.NameChanged += value;
                remove => ViewModel.NameChanged -= value;
            }

            /// <inheritdoc />
            public event EventHandler? InStockChanged
            {
                add => ViewModel.InStockChanged += value;
                remove => ViewModel.InStockChanged -= value;
            }

            /// <inheritdoc />
            public void ApplyIngredient(Ingredient ingredient)
            {
                ArgumentNullException.ThrowIfNull(ingredient);

                ViewModel.Name = ingredient.Name;
                ViewModel.InStock = ingredient.InStock;

                ApplyDishes(ingredient);
            }

            private void ApplyDishes(Ingredient ingredient)
            {
                List<Dish> dishes = ingredient.DishIngredients.Select(x => x.Dish).OfType<Dish>().ToList();

                ViewModel.AvailableDishesGrid.Api.SetSuppliedDishes(dishes);
                ViewModel.SuppliedDishesGrid.Api.SetSuppliedDishes(dishes);
            }

            /// <inheritdoc />
            public void SetReadOnly(bool isReadOnly)
            {
                ViewModel.IsReadOnly = isReadOnly;
            }

            #endregion

            private void OnIsReadOnlyChanged(object? sender, EventArgs e)
            {
                UpdateReadOnlyState();
            }

            internal void UpdateReadOnlyState()
            {
                ViewModel.NameTextBox.IsReadOnly = ViewModel.IsReadOnly;
                ViewModel.InStockCheckBox.IsEnabled = !ViewModel.IsReadOnly;
            }
        }
    }
}
