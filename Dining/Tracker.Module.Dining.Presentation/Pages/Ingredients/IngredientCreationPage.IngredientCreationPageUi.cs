using Tracker.Module.Dining.Presentation.Pieces.Ingredients;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Ingredients
{
    internal sealed partial class IngredientCreationPage
    {
        private sealed class IngredientCreationPageUi(
            IngredientCreationPageLogic logic,
            IngredientCreationPageViewModel viewModel)
            : BaseUi<IngredientCreationPageLogic, IngredientCreationPageViewModel>(logic, viewModel)
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
                grid.Children.Add(CreateIngredientEditor().SetRow(1));
            }

            private UIElement CreateHeader()
            {
                return TextBlockFactory.CreateHeader("Create Ingredient");
            }

            private IngredientEditor CreateIngredientEditor()
            {
                IngredientEditor.IngredientEditorArguments arguments =
                    ViewModel.Arguments.ArgumentsFactory.CreateIngredientEditorArguments(isReadOnly: false);

                ViewModel.IngredientEditor = new IngredientEditor(arguments);

                return ViewModel.IngredientEditor;
            }

            private Grid CreateButtonsGrid()
            {
                return SimplePieceFactory.CreateSaveCancelButtonGrid(Logic.SaveClicked, Logic.CancelClicked);
            }
        }
    }
}