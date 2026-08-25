using Tracker.Module.Dining.Presentation.Pieces.Ingredients;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Ingredients
{
    internal sealed partial class IngredientsPage
    {
        internal sealed class IngredientsPageUi(IngredientsPageLogic logic, IngredientsPageViewModel viewModel)
            : BaseUi<IngredientsPageLogic, IngredientsPageViewModel>(logic, viewModel)
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
                grid.Children.Add(CreateIngredientsGrid().SetRow(1));
            }

            private IngredientsGrid CreateIngredientsGrid()
            {
                ViewModel.IngredientsGrid = new IngredientsGrid(
                    ViewModel.Arguments.ArgumentsFactory.CreateIngredientsGridArguments());

                return ViewModel.IngredientsGrid;
            }

            private Grid CreateHeaderGrid()
            {
                Grid grid = GridFactory.CreateDefaultGrid();

                grid.DefineColumns(new GridLength(1, GridUnitType.Star));

                grid.Children.Add(CreateIngredientButton().SetColumn(0));
                grid.Children.Add(TextBlockFactory.CreateHeader("Ingredients").SetColumn(0));
                grid.Children.Add(CreateShowDetailsButton().SetColumn(0));

                return grid;
            }

            private Button CreateIngredientButton()
            {
                return ButtonFactory.CreateButton(Symbol.Add, "Create Ingredient", Logic.CreateIngredientClicked,
                    HorizontalAlignment.Left);
            }

            private Button CreateShowDetailsButton()
            {
                return ButtonFactory.CreateButton(Symbol.OpenPane, "Show Details", Logic.ShowDetailsClicked,
                    HorizontalAlignment.Right);
            }
        }
    }
}
