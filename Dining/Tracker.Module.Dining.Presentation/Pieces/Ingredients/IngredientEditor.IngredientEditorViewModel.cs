using CommunityToolkit.Mvvm.ComponentModel;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Presentation.Pieces.Dishes;
using Tracker.Shared.Frontend.Pieces;

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients;

internal sealed partial class IngredientEditor
{
    internal sealed partial class IngredientEditorViewModel(IngredientEditorArguments arguments) : ObservableObject
    {
        [ObservableProperty] private bool inStock = arguments.Ingredient?.InStock ?? false;

        [ObservableProperty] private bool isReadOnly = arguments.IsReadOnly;

        [ObservableProperty] private string name = arguments.Ingredient?.Name ?? string.Empty;
        internal IngredientEditorArguments Arguments { get; } = arguments;

        internal TextBox NameTextBox { get; set; } = null!;
        internal CheckBox InStockCheckBox { get; set; } = null!;

        internal DishesGrid AvailableDishesGrid { get; set; } = null!;
        internal DishesGrid SuppliedDishesGrid { get; set; } = null!;
        internal EntitySelectionPiece<Dish> DishSelector { get; set; } = null!;

        public event EventHandler? IsReadOnlyChanged;
        public event EventHandler? NameChanged;
        public event EventHandler? InStockChanged;

        partial void OnIsReadOnlyChanged(bool value)
        {
            IsReadOnlyChanged?.Invoke(this, EventArgs.Empty);
        }

        partial void OnNameChanged(string value)
        {
            NameChanged?.Invoke(this, EventArgs.Empty);
        }

        partial void OnInStockChanged(bool value)
        {
            InStockChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
