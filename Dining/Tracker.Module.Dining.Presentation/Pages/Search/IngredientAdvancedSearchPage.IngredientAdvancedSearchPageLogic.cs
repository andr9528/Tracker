using Microsoft.Extensions.Logging;
using Tracker.Shared.Frontend.Core;

namespace Tracker.Module.Dining.Presentation.Pages.Search;

internal sealed partial class IngredientAdvancedSearchPage
{
    internal sealed class IngredientAdvancedSearchPageLogic
        : BaseLogic<IngredientAdvancedSearchPageViewModel>
    {
        private readonly ILogger<IngredientAdvancedSearchPageLogic> logger;

        public IngredientAdvancedSearchPageLogic(IngredientAdvancedSearchPageViewModel viewModel) : base(viewModel)
        {
            logger = ViewModel.Arguments.LoggerFactory.CreateLogger<IngredientAdvancedSearchPageLogic>();
        }

        private void ApplyNameSearchMode()
        {
            string name = ViewModel.Arguments.Searchable.Name ??
                          ViewModel.Arguments.Searchable.Searchable.Name ?? string.Empty;

            if (ViewModel.UseFuzzySearch)
            {
                ViewModel.Arguments.Searchable.Name = name;
                ViewModel.Arguments.Searchable.Searchable.Name = string.Empty;
            }
            else
            {
                ViewModel.Arguments.Searchable.Name = null;
                ViewModel.Arguments.Searchable.Searchable.Name = name;
            }
        }

        public void ResetClicked(object sender, RoutedEventArgs e)
        {
            ViewModel.UseFuzzySearch = true;
            ViewModel.SelectedInStock = null;
            ViewModel.InStockOptionBar.ViewModel.SelectedValue = null;
            ViewModel.InStockOptionBar.ViewModel.EitherButton.IsChecked = true;
            ViewModel.MinimumDishCountText = string.Empty;
            ViewModel.MaximumDishCountText = string.Empty;
            ViewModel.ValidationMessage = null;

            ViewModel.Arguments.Searchable.InStock = null;
            ViewModel.Arguments.Searchable.MinimumDishCount = null;
            ViewModel.Arguments.Searchable.MaximumDishCount = null;

            ApplyNameSearchMode();
        }

        public void CancelClicked(object sender, RoutedEventArgs e)
        {
            ViewModel.Arguments.NavigationService.NavigateBack();
        }

        public void ApplyClicked(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.TryGetDishCountRange(out int? minimum, out int? maximum))
            {
                return;
            }

            ApplyNameSearchMode();

            ViewModel.Arguments.Searchable.InStock = ViewModel.SelectedInStock;
            ViewModel.Arguments.Searchable.MinimumDishCount = minimum;
            ViewModel.Arguments.Searchable.MaximumDishCount = maximum;

            logger.LogInformation(
                "Applied advanced Ingredient search. Fuzzy: {UseFuzzySearch}, InStock: {InStock}, MinimumDishCount: {MinimumDishCount}, MaximumDishCount: {MaximumDishCount}",
                ViewModel.UseFuzzySearch, ViewModel.Arguments.Searchable.InStock, minimum, maximum);

            ViewModel.Arguments.NavigationService.NavigateBack();
        }
    }
}
