using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Frontend.Core;

namespace Tracker.Module.Dining.Presentation.Pieces.Dishes;

internal sealed partial class DishEditor
{
    internal sealed class DishEditorLogic : BaseLogic<DishEditorViewModel>, IDishEditorApi
    {
        public DishEditorLogic(DishEditorViewModel viewModel) : base(viewModel)
        {
            ViewModel.IsReadOnlyChanged += OnIsReadOnlyChanged;
        }

        #region Implementation of IDishEditorApi

        /// <inheritdoc />
        public string Name => ViewModel.Name;

        /// <inheritdoc />
        public IReadOnlyCollection<Ingredient> Ingredients =>
            ViewModel.SuppliedIngredientsGrid.Api.SuppliedIngredients;

        /// <inheritdoc />
        public event EventHandler? NameChanged
        {
            add => ViewModel.NameChanged += value;
            remove => ViewModel.NameChanged -= value;
        }

        /// <inheritdoc />
        public void ApplyDish(Dish dish)
        {
            ArgumentNullException.ThrowIfNull(dish);

            ViewModel.Name = dish.Name;
            ApplyIngredients(dish);
        }

        /// <inheritdoc />
        public void SetReadOnly(bool isReadOnly)
        {
            ViewModel.IsReadOnly = isReadOnly;
        }

        #endregion

        internal void ApplyInitialIngredients()
        {
            Dish? dish = ViewModel.Arguments.Dish;

            if (dish is null)
            {
                return;
            }

            ApplyIngredients(dish);
        }

        private void ApplyIngredients(Dish dish)
        {
            List<Ingredient> ingredients = dish.DishIngredients.Select(x => x.Ingredient).OfType<Ingredient>().ToList();

            ViewModel.AvailableIngredientsGrid.Api.SetSuppliedIngredients(ingredients);
            ViewModel.SuppliedIngredientsGrid.Api.SetSuppliedIngredients(ingredients);
        }

        private void OnIsReadOnlyChanged(object? sender, EventArgs e)
        {
            UpdateReadOnlyState();
        }

        internal void UpdateReadOnlyState()
        {
            ViewModel.NameTextBox.IsReadOnly = ViewModel.IsReadOnly;
        }
    }
}
