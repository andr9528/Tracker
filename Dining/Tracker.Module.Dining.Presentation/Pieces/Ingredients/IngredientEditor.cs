using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients
{
    internal sealed partial class IngredientEditor : Border
    {
        private IngredientEditorViewModel ViewModel =>
            (IngredientEditorViewModel) DataContext;

        private IngredientEditorLogic Logic { get; }

        private IngredientEditorUi Ui { get; }

        public IIngredientEditorApi Api => Logic;

        public IngredientEditor(IngredientEditorArguments arguments)
        {
            ArgumentNullException.ThrowIfNull(arguments);
            this.ConfigurePieceBorder();

            DataContext = new IngredientEditorViewModel(arguments);

            Logic = new IngredientEditorLogic(ViewModel);
            Ui = new IngredientEditorUi(Logic, ViewModel);

            Child = Ui.CreateContentGrid();

            Logic.UpdateReadOnlyState();
        }

        internal sealed record IngredientEditorArguments(Ingredient? Ingredient = null, bool IsReadOnly = true);

        internal interface IIngredientEditorApi
        {
            string Name { get; }

            bool InStock { get; }

            event EventHandler? NameChanged;
            event EventHandler? InStockChanged;

            void ApplyIngredient(Ingredient ingredient);

            void SetReadOnly(bool isReadOnly);
        }
    }
}
