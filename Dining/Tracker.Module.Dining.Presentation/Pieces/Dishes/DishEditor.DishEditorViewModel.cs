using CommunityToolkit.Mvvm.ComponentModel;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Presentation.Pieces.Ingredients;
using Tracker.Shared.Frontend.Pieces;

namespace Tracker.Module.Dining.Presentation.Pieces.Dishes;

internal sealed partial class DishEditor
{
    internal sealed partial class DishEditorViewModel(DishEditorArguments arguments) : ObservableObject
    {
        [ObservableProperty] private bool isReadOnly = arguments.IsReadOnly;

        [ObservableProperty] private string name = arguments.Dish?.Name ?? string.Empty;
        internal DishEditorArguments Arguments { get; } = arguments;

        internal TextBox NameTextBox { get; set; } = null!;

        internal IngredientsGrid AvailableIngredientsGrid { get; set; } = null!;
        internal IngredientsGrid SuppliedIngredientsGrid { get; set; } = null!;
        internal EntitySelectionPiece<Ingredient> IngredientSelector { get; set; } = null!;

        public event EventHandler? IsReadOnlyChanged;
        public event EventHandler? NameChanged;

        partial void OnIsReadOnlyChanged(bool value)
        {
            IsReadOnlyChanged?.Invoke(this, EventArgs.Empty);
        }

        partial void OnNameChanged(string value)
        {
            NameChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
