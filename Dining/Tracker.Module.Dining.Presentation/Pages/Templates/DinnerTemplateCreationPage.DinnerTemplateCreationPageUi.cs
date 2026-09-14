using Tracker.Module.Dining.Presentation.Pieces.Dinners;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Templates;

internal sealed partial class DinnerTemplateCreationPage
{
    private sealed class DinnerTemplateCreationPageUi(
        DinnerTemplateCreationPageLogic logic,
        DinnerTemplateCreationPageViewModel viewModel)
        : BaseUi<DinnerTemplateCreationPageLogic, DinnerTemplateCreationPageViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            ConfigureDefaultPageGrid(grid);

            grid.DefineRows(GridLength.Auto, new GridLength(1, GridUnitType.Star));

            grid.DefineColumns(new GridLength(1, GridUnitType.Star));
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(CreateHeader().SetRow(0));
            grid.Children.Add(CreateButtonsGrid().SetRow(0));
            grid.Children.Add(CreateDinnerTemplateEditor().SetRow(1));
        }

        private UIElement CreateHeader()
        {
            return TextBlockFactory.CreateHeader("Create Dinner Template");
        }

        private DinnerTemplateEditor CreateDinnerTemplateEditor()
        {
            DinnerTemplateEditor.DinnerTemplateEditorArguments arguments = new(IsReadOnly: false);

            ViewModel.DinnerTemplateEditor = new DinnerTemplateEditor(arguments);

            return ViewModel.DinnerTemplateEditor;
        }

        private Grid CreateButtonsGrid()
        {
            return SimplePieceFactory.CreateSaveCancelButtonGrid(Logic.SaveClicked, Logic.CancelClicked);
        }
    }
}
