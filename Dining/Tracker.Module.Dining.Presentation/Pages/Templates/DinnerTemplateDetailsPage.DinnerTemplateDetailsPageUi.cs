using Tracker.Module.Dining.Presentation.Pieces.Dinners;
using Tracker.Shared.Frontend.Core.Details;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Templates;

internal sealed partial class DinnerTemplateDetailsPage
{
    internal sealed class DinnerTemplateDetailsPageUi(
        DinnerTemplateDetailsPageLogic logic,
        DinnerTemplateDetailsPageViewModel viewModel)
        : BaseDetailsPageUi<DinnerTemplateDetailsPageLogic, DinnerTemplateDetailsPageViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            ConfigureDefaultPageGrid(grid);

            grid.DefineRows(GridLength.Auto, new GridLength(1, GridUnitType.Star));
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(CreateHeader().SetRow(0));
            grid.Children.Add(CreateDetailsButtonsGrid().SetRow(0));
            grid.Children.Add(CreateDinnerTemplateEditor().SetRow(1));
        }

        private UIElement CreateHeader()
        {
            return TextBlockFactory.CreateHeader("Dinner Template Details");
        }

        private DinnerTemplateEditor CreateDinnerTemplateEditor()
        {
            DinnerTemplateEditor.DinnerTemplateEditorArguments arguments = new();

            ViewModel.DinnerTemplateEditor = new DinnerTemplateEditor(arguments);

            Logic.RegisterDinnerTemplateEditorEvents();

            return ViewModel.DinnerTemplateEditor;
        }
    }
}
