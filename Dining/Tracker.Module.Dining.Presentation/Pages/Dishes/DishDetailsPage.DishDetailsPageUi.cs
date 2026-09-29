using Tracker.Module.Dining.Presentation.Pieces.Dishes;
using Tracker.Shared.Frontend.Core.Details;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Dishes;

internal sealed partial class DishDetailsPage
{
    internal sealed class DishDetailsPageUi(DishDetailsPageLogic logic, DishDetailsPageViewModel viewModel)
        : BaseDetailsPageUi<DishDetailsPageLogic, DishDetailsPageViewModel>(logic, viewModel)
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
            grid.Children.Add(CreateDishEditor().SetRow(1));
        }

        private UIElement CreateHeader()
        {
            return TextBlockFactory.CreateHeader("Dish Details");
        }

        private DishEditor CreateDishEditor()
        {
            DishEditor.DishEditorArguments arguments = ViewModel.Arguments.ArgumentsFactory.CreateDishEditorArguments();

            ViewModel.DishEditor = new DishEditor(arguments);

            Logic.RegisterDishEditorEvents();

            return ViewModel.DishEditor;
        }
    }
}
