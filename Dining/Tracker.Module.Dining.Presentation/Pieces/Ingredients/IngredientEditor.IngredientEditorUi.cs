using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Presentation.Pieces.Dishes;
using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;
using Tracker.Shared.Frontend.Pieces;

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients
{
    internal sealed partial class IngredientEditor
    {
        internal sealed class IngredientEditorUi(IngredientEditorLogic logic, IngredientEditorViewModel viewModel)
            : BaseUi<IngredientEditorLogic, IngredientEditorViewModel>(logic, viewModel)
        {
            protected override void ConfigureGrid(Grid grid)
            {
                grid.RowSpacing = 8;
                grid.ColumnSpacing = 8;

                grid.DefineRows(GridLength.Auto, GridLength.Auto, new GridLength(1, GridUnitType.Star));
            }

            protected override void AddControlsToGrid(Grid grid)
            {
                grid.Children.Add(CreateNameTextBox().SetRow(0));
                grid.Children.Add(CreateInStockCheckBox().SetRow(1));
                grid.Children.Add(CreateDishSelector().SetRow(2));
            }

            private EntitySelectionPiece<Dish> CreateDishSelector()
            {
                CreateDishGrids();

                EntitySelectionPiece<Dish>.EntitySelectionPieceArguments arguments =
                    ViewModel.Arguments.ArgumentsFactory.CreateEntitySelectionPieceArguments($"{nameof(Dish)}es",
                        ViewModel.AvailableDishesGrid, ViewModel.SuppliedDishesGrid);

                ViewModel.DishSelector = new EntitySelectionPiece<Dish>(arguments);

                return ViewModel.DishSelector;
            }

            private void CreateDishGrids()
            {
                ViewModel.AvailableDishesGrid = new DishesGrid(
                    ViewModel.Arguments.ArgumentsFactory.CreateDishesGridArguments(GridEntitySource.NOT_SUPPLIED,
                        GridDisplayMode.COMPACT));

                ViewModel.SuppliedDishesGrid = new DishesGrid(
                    ViewModel.Arguments.ArgumentsFactory.CreateDishesGridArguments(GridEntitySource.SUPPLIED,
                        GridDisplayMode.COMPACT));
            }

            private TextBox CreateNameTextBox()
            {
                ViewModel.NameTextBox = TextBoxFactory.CreateSearchBox("Name", "Ingredient name...",
                    nameof(IngredientEditorViewModel.Name));

                return ViewModel.NameTextBox;
            }

            private Grid CreateInStockCheckBox()
            {
                Grid grid = CheckBoxFactory.CreateLightCheckBoxWithLabel("In stock?",
                    nameof(IngredientEditorViewModel.InStock), out CheckBox checkBox);

                ViewModel.InStockCheckBox = checkBox;

                return grid;
            }
        }
    }
}
