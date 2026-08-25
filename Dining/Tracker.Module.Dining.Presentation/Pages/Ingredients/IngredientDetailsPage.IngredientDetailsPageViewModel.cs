using CommunityToolkit.Mvvm.ComponentModel;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Presentation.Pieces.Ingredients;
using Tracker.Shared.Frontend.Core.Details;

namespace Tracker.Module.Dining.Presentation.Pages.Ingredients
{
    internal sealed partial class IngredientDetailsPage
    {
        internal sealed partial class IngredientDetailsPageViewModel(IngredientDetailsPageArguments arguments)
            : BaseDetailsPageViewModel
        {
            public IngredientDetailsPageArguments Arguments { get; } = arguments;

            [ObservableProperty] private Ingredient ingredient = null!;

            public IngredientEditor IngredientEditor { get; set; } = null!;
        }
    }
}