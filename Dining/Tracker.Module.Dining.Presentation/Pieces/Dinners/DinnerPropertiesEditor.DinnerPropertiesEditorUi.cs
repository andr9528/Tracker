using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Dinners;

internal sealed partial class DinnerPropertiesEditor
{
    internal sealed class DinnerPropertiesEditorUi(
        DinnerPropertiesEditorLogic logic,
        DinnerPropertiesEditorViewModel viewModel)
        : BaseUi<DinnerPropertiesEditorLogic, DinnerPropertiesEditorViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            grid.RowSpacing = 8;
            grid.ColumnSpacing = 24;

            grid.DefineRows(GridLength.Auto, GridLength.Auto, GridLength.Auto);

            grid.DefineColumns(new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star));
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(CreateCheckBox("Take away?", nameof(DinnerPropertiesEditorViewModel.IsTakeAway)).SetRow(0)
                .SetColumn(0));

            grid.Children.Add(CreateCheckBox("Eaten out?", nameof(DinnerPropertiesEditorViewModel.IsEatenOut)).SetRow(1)
                .SetColumn(0));

            grid.Children.Add(
                CreateCheckBox("Ready made dish?", nameof(DinnerPropertiesEditorViewModel.IsReadyMadeDish)).SetRow(2)
                    .SetColumn(0));

            grid.Children.Add(CreateCheckBox("Has leftovers?", nameof(DinnerPropertiesEditorViewModel.HasLeftovers))
                .SetRow(0).SetColumn(1));

            grid.Children.Add(CreateCheckBox("Enough for dinner?",
                nameof(DinnerPropertiesEditorViewModel.LeftoversEnoughForDinner)).SetRow(1).SetColumn(1));

            grid.Children.Add(CreateCheckBox("Is leftovers?", nameof(DinnerPropertiesEditorViewModel.IsLeftovers))
                .SetRow(2).SetColumn(1));
        }

        private Grid CreateCheckBox(string label, string bindingPath)
        {
            Grid grid = CheckBoxFactory.CreateLightCheckBoxWithLabel(label, bindingPath, out CheckBox checkBox);

            ViewModel.CheckBoxes.Add(checkBox);

            return grid;
        }
    }
}
