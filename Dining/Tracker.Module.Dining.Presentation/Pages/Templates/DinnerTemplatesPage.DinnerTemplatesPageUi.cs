using Tracker.Module.Dining.Presentation.Pieces.Dinners;
using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Templates;

internal sealed partial class DinnerTemplatesPage
{
    internal sealed class DinnerTemplatesPageUi(DinnerTemplatesPageLogic logic, DinnerTemplatesPageViewModel viewModel)
        : BaseUi<DinnerTemplatesPageLogic, DinnerTemplatesPageViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            grid.Padding = new Thickness(16);
            grid.RowSpacing = 16;

            grid.DefineRows(GridLength.Auto, new GridLength(1, GridUnitType.Star));
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(CreateHeaderGrid().SetRow(0));
            grid.Children.Add(CreateDinnerTemplatesGrid().SetRow(1));
        }

        private DinnerTemplatesGrid CreateDinnerTemplatesGrid()
        {
            ViewModel.DinnerTemplatesGrid = new DinnerTemplatesGrid(
                ViewModel.Arguments.ArgumentsFactory.CreateDinnerTemplatesGridArguments(GridDisplayMode.NORMAL));

            return ViewModel.DinnerTemplatesGrid;
        }

        private Grid CreateHeaderGrid()
        {
            Grid grid = GridFactory.CreateDefaultGrid();

            grid.DefineColumns(new GridLength(1, GridUnitType.Star));

            grid.Children.Add(CreateDinnerTemplateButton().SetColumn(0));
            grid.Children.Add(TextBlockFactory.CreateHeader("Dinner Templates").SetColumn(0));
            grid.Children.Add(CreateShowDetailsButton().SetColumn(0));

            return grid;
        }

        private Button CreateDinnerTemplateButton()
        {
            return ButtonFactory.CreateButton(Symbol.Add, "Create Dinner Template", Logic.CreateDinnerTemplateClicked,
                HorizontalAlignment.Left);
        }

        private Button CreateShowDetailsButton()
        {
            return ButtonFactory.CreateButton(Symbol.OpenPane, "Show Details", Logic.ShowDetailsClicked,
                HorizontalAlignment.Right);
        }
    }
}
