using CommunityToolkit.Mvvm.ComponentModel;
using Tracker.Module.Dining.Presentation.Pieces.Dishes;

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients
{
    internal sealed partial class IngredientEditor
    {
        internal sealed partial class IngredientEditorViewModel(IngredientEditorArguments arguments) : ObservableObject
        {
            internal IngredientEditorArguments Arguments { get; } = arguments;

            public event EventHandler? IsReadOnlyChanged;
            public event EventHandler? NameChanged;
            public event EventHandler? InStockChanged;

            internal TextBox NameTextBox { get; set; } = null!;
            internal CheckBox InStockCheckBox { get; set; } = null!;

            internal DishesGrid AvailableDishesGrid { get; set; } = null!;
            internal DishesGrid SuppliedDishesGrid { get; set; } = null!;

            [ObservableProperty] private string name = arguments.Ingredient?.Name ?? string.Empty;

            [ObservableProperty] private bool inStock = arguments.Ingredient?.InStock ?? false;

            [ObservableProperty] private bool isReadOnly = arguments.IsReadOnly;

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
}
