using CommunityToolkit.Mvvm.ComponentModel;
using Tracker.Module.Dining.Presentation.Pieces.Ingredients;

namespace Tracker.Module.Dining.Presentation.Pages.Ingredients;

internal sealed partial class IngredientsPage
{
    internal sealed class IngredientsPageViewModel(IngredientsPageArguments arguments) : ObservableObject
    {
        public IngredientsPageArguments Arguments { get; } = arguments;

        internal IngredientsGrid IngredientsGrid { get; set; } = null!;
    }
}
