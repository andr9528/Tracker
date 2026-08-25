using Tracker.Module.Dining.Presentation.Pieces.Ingredients;

namespace Tracker.Module.Dining.Presentation.Pages.Ingredients
{
    internal sealed partial class IngredientCreationPage
    {
        private sealed class IngredientCreationPageViewModel(IngredientCreationPageArguments arguments)
        {
            public IngredientCreationPageArguments Arguments { get; } = arguments;

            internal IngredientEditor IngredientEditor { get; set; } = null!;
        }
    }
}