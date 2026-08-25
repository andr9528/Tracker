using Tracker.Shared.Frontend.Core;

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients
{
    internal sealed partial class IngredientEditor
    {
        internal sealed class IngredientEditorLogic : BaseLogic<IngredientEditorViewModel>
        {
            public IngredientEditorLogic(IngredientEditorViewModel viewModel) : base(viewModel)
            {
                ViewModel.IsReadOnlyChanged += OnIsReadOnlyChanged;
            }

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