using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Abstraction.Attributes;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Ingredients
{
    /// <summary>
    /// Creates a new ingredient. Editing is always enabled.
    /// </summary>
    [SkipOnBackNavigation]
    internal sealed partial class IngredientCreationPage : Border
    {
        public IngredientCreationPage(IngredientCreationPageArguments arguments)
        {
            ArgumentNullException.ThrowIfNull(arguments);

            DataContext = new IngredientCreationPageViewModel(arguments);
            this.ConfigurePageBorder();

            var viewModel = (IngredientCreationPageViewModel) DataContext;
            var logic = new IngredientCreationPageLogic(viewModel);
            var ui = new IngredientCreationPageUi(logic, viewModel);

            Child = ui.CreateContentGrid();
        }

        internal sealed record IngredientCreationPageArguments(
            IEntityQueryService<Ingredient, SearchableIngredient> IngredientQueryService,
            INavigationService NavigationService,
            ILoggerFactory LoggerFactory,
            DiningArgumentsFactory ArgumentsFactory);
    }
}
