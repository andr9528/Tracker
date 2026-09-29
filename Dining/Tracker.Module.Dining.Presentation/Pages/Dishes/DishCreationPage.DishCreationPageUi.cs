using Tracker.Module.Dining.Presentation.Pieces.Dishes;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Dishes;

internal sealed partial class DishCreationPage
{
    private sealed class DishCreationPageUi(DishCreationPageLogic logic, DishCreationPageViewModel viewModel)
        : BaseUi<DishCreationPageLogic, DishCreationPageViewModel>(logic, viewModel)
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
            grid.Children.Add(CreateDishEditor().SetRow(1));
        }

        private UIElement CreateHeader()
        {
            return TextBlockFactory.CreateHeader("Create Dish");
        }

        private DishEditor CreateDishEditor()
        {
            DishEditor.DishEditorArguments arguments =
                ViewModel.Arguments.ArgumentsFactory.CreateDishEditorArguments(isReadOnly: false);

            ViewModel.DishEditor = new DishEditor(arguments);

            return ViewModel.DishEditor;
        }

        private Grid CreateButtonsGrid()
        {
            return SimplePieceFactory.CreateSaveCancelButtonGrid(Logic.SaveClicked, Logic.CancelClicked);
        }
    }
}
