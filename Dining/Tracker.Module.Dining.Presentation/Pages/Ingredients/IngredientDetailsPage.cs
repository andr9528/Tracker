using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Ingredients
{
    internal sealed partial class IngredientDetailsPage : Border
    {
        public IngredientDetailsPage(IngredientDetailsPageArguments arguments)
        {
            ArgumentNullException.ThrowIfNull(arguments);
            this.ConfigurePageBorder();

            DataContext = new IngredientDetailsPageViewModel(arguments);

            var viewModel = (IngredientDetailsPageViewModel) DataContext;
            var logic = new IngredientDetailsPageLogic(viewModel);
            var ui = new IngredientDetailsPageUi(logic, viewModel);

            Child = ui.CreateContentGrid();

            _ = logic.RefreshIngredient();
        }

        internal sealed record IngredientDetailsPageArguments(
            int IngredientId,
            IEntityQueryService<Ingredient, SearchableIngredient> IngredientQueryService,
            IUiDispatcher UiDispatcher,
            ILoggerFactory LoggerFactory,
            INavigationService NavigationService,
            DiningArgumentsFactory ArgumentsFactory);
    }
}
