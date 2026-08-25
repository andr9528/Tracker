using Tracker.Module.Dining.Presentation.Pieces.Ingredients;
using Tracker.Shared.Frontend.Core.Details;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Ingredients
{
    internal sealed partial class IngredientDetailsPage
    {
        internal sealed class IngredientDetailsPageUi(
            IngredientDetailsPageLogic logic,
            IngredientDetailsPageViewModel viewModel)
            : BaseDetailsPageUi<IngredientDetailsPageLogic, IngredientDetailsPageViewModel>(logic, viewModel)
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
                grid.Children.Add(CreateIngredientEditor().SetRow(1));
            }

            private UIElement CreateHeader()
            {
                return TextBlockFactory.CreateHeader("Ingredient Details");
            }

            private IngredientEditor CreateIngredientEditor()
            {
                IngredientEditor.IngredientEditorArguments arguments =
                    ViewModel.Arguments.ArgumentsFactory.CreateIngredientEditorArguments();

                ViewModel.IngredientEditor = new IngredientEditor(arguments);

                Logic.RegisterIngredientEditorEvents();

                return ViewModel.IngredientEditor;
            }
        }
    }
}