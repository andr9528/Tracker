using CommunityToolkit.Mvvm.ComponentModel;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Presentation.Pieces.Dishes;
using Tracker.Shared.Frontend.Core.Details;

namespace Tracker.Module.Dining.Presentation.Pages.Dishes;

internal sealed partial class DishDetailsPage
{
    internal sealed partial class DishDetailsPageViewModel(DishDetailsPageArguments arguments)
        : BaseDetailsPageViewModel
    {
        public DishDetailsPageArguments Arguments { get; } = arguments;

        [ObservableProperty] private Dish dish = null!;

        public DishEditor DishEditor { get; set; } = null!;
    }
}
