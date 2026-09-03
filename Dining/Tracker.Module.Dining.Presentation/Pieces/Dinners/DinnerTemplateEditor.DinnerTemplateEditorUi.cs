using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Dinners;

internal sealed partial class DinnerTemplateEditor
{
    internal sealed class DinnerTemplateEditorUi(
        DinnerTemplateEditorLogic logic,
        DinnerTemplateEditorViewModel viewModel)
        : BaseUi<DinnerTemplateEditorLogic, DinnerTemplateEditorViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            grid.RowSpacing = 16;

            grid.DefineRows(GridLength.Auto, GridLength.Auto);
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(CreateNameTextBox().SetRow(0));

            grid.Children.Add(CreateDinnerPropertiesEditor().SetRow(1));
        }

        private TextBox CreateNameTextBox()
        {
            ViewModel.NameTextBox = TextBoxFactory.CreateSearchBox("Name", "Dinner template name...",
                nameof(DinnerTemplateEditorViewModel.Name));

            return ViewModel.NameTextBox;
        }

        private DinnerPropertiesEditor CreateDinnerPropertiesEditor()
        {
            DinnerPropertiesEditor.DinnerPropertiesEditorArguments arguments = new(ViewModel.Arguments.DinnerTemplate,
                ViewModel.IsReadOnly);

            var editor = new DinnerPropertiesEditor(arguments);

            ViewModel.PropertiesEditor = editor;

            return editor;
        }
    }
}
