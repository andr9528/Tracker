using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Presentation.Pieces.Ingredients;
using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;
using Tracker.Shared.Frontend.Pieces;

namespace Tracker.Module.Dining.Presentation.Pieces.Dishes;

internal sealed partial class DishEditor
{
    internal sealed class DishEditorUi(DishEditorLogic logic, DishEditorViewModel viewModel)
        : BaseUi<DishEditorLogic, DishEditorViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            grid.RowSpacing = 8;
            grid.ColumnSpacing = 8;

            grid.DefineRows(GridLength.Auto, new GridLength(1, GridUnitType.Star));
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(CreateNameTextBox().SetRow(0));
            grid.Children.Add(CreateIngredientSelector().SetRow(1));
        }

        private TextBox CreateNameTextBox()
        {
            ViewModel.NameTextBox = TextBoxFactory.CreateSearchBox(
                "Name", "Dish name...", nameof(DishEditorViewModel.Name));

            return ViewModel.NameTextBox;
        }

        private EntitySelectionPiece<Ingredient> CreateIngredientSelector()
        {
            CreateIngredientGrids();

            EntitySelectionPiece<Ingredient>.EntitySelectionPieceArguments arguments =
                ViewModel.Arguments.ArgumentsFactory.CreateEntitySelectionPieceArguments($"{nameof(Ingredient)}s",
                    ViewModel.AvailableIngredientsGrid, ViewModel.SuppliedIngredientsGrid);

            return new EntitySelectionPiece<Ingredient>(arguments);
        }

        private void CreateIngredientGrids()
        {
            ViewModel.AvailableIngredientsGrid = new IngredientsGrid(
                ViewModel.Arguments.ArgumentsFactory.CreateIngredientsGridArguments(GridEntitySource.NOT_SUPPLIED,
                    GridDisplayMode.COMPACT));

            ViewModel.SuppliedIngredientsGrid = new IngredientsGrid(
                ViewModel.Arguments.ArgumentsFactory.CreateIngredientsGridArguments(GridEntitySource.SUPPLIED,
                    GridDisplayMode.COMPACT));
        }
    }
}
