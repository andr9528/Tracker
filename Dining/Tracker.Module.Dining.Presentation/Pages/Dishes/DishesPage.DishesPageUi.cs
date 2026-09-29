using Tracker.Module.Dining.Presentation.Pieces.Dishes;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Dishes;

internal sealed partial class DishesPage
{
    internal sealed class DishesPageUi(DishesPageLogic logic, DishesPageViewModel viewModel)
        : BaseUi<DishesPageLogic, DishesPageViewModel>(logic, viewModel)
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
            grid.Children.Add(CreateDishesGrid().SetRow(1));
        }

        private DishesGrid CreateDishesGrid()
        {
            ViewModel.DishesGrid = new DishesGrid(ViewModel.Arguments.ArgumentsFactory.CreateDishesGridArguments());

            return ViewModel.DishesGrid;
        }

        private Grid CreateHeaderGrid()
        {
            Grid grid = GridFactory.CreateDefaultGrid();

            grid.DefineColumns(new GridLength(1, GridUnitType.Star));

            grid.Children.Add(CreateDishButton().SetColumn(0));
            grid.Children.Add(TextBlockFactory.CreateHeader("Dishes").SetColumn(0));
            grid.Children.Add(CreateShowDetailsButton().SetColumn(0));

            return grid;
        }

        private Button CreateDishButton()
        {
            return ButtonFactory.CreateButton(Symbol.Add, "Create Dish", Logic.CreateDishClicked,
                HorizontalAlignment.Left);
        }

        private Button CreateShowDetailsButton()
        {
            return ButtonFactory.CreateButton(Symbol.OpenPane, "Show Details", Logic.ShowDetailsClicked,
                HorizontalAlignment.Right);
        }
    }
}
