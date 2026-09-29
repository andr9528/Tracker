using Tracker.Module.Dining.Presentation.Pieces.Dishes;

namespace Tracker.Module.Dining.Presentation.Pages.Dishes;

internal sealed partial class DishCreationPage
{
    private sealed class DishCreationPageViewModel(DishCreationPageArguments arguments)
    {
        public DishCreationPageArguments Arguments { get; } = arguments;

        internal DishEditor DishEditor { get; set; } = null!;
    }
}
