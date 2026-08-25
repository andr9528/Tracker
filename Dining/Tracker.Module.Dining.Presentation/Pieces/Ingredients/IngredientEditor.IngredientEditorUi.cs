using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients
{
    internal sealed partial class IngredientEditor
    {
        internal sealed class IngredientEditorUi(IngredientEditorLogic logic, IngredientEditorViewModel viewModel)
            : BaseUi<IngredientEditorLogic, IngredientEditorViewModel>(logic, viewModel)
        {
            protected override void ConfigureGrid(Grid grid)
            {
                grid.RowSpacing = 8;
                grid.ColumnSpacing = 8;

                grid.DefineRows(GridLength.Auto, GridLength.Auto);
            }

            protected override void AddControlsToGrid(Grid grid)
            {
                grid.Children.Add(CreateNameTextBox().SetRow(0));
                grid.Children.Add(CreateInStockCheckBox().SetRow(1));
            }

            private TextBox CreateNameTextBox()
            {
                ViewModel.NameTextBox = TextBoxFactory.CreateSearchBox("Name", "Ingredient name...",
                    nameof(IngredientEditorViewModel.Name));

                return ViewModel.NameTextBox;
            }

            private Grid CreateInStockCheckBox()
            {
                Grid grid = CheckBoxFactory.CreateLightCheckBoxWithLabel("In stock?",
                    nameof(IngredientEditorViewModel.InStock), out CheckBox checkBox);

                ViewModel.InStockCheckBox = checkBox;

                return grid;
            }
        }
    }
}
