using CommunityToolkit.Mvvm.ComponentModel;
using Tracker.Module.Dining.Presentation.Pieces.Dishes;

namespace Tracker.Module.Dining.Presentation.Pages.Dishes;

internal sealed partial class DishesPage
{
    internal sealed partial class DishesPageViewModel(DishesPageArguments arguments) : ObservableObject
    {
        public DishesPageArguments Arguments { get; } = arguments;

        internal DishesGrid DishesGrid { get; set; } = null!;
    }
}
