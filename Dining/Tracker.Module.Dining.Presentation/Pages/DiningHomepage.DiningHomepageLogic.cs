using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Abstraction.Records;
using Tracker.Shared.Frontend.Core;

namespace Tracker.Module.Dining.Presentation.Pages;

internal sealed partial class DiningHomepage
{
    internal sealed class DiningHomepageLogic : BaseLogic<DiningHomepageViewModel>
    {
        private readonly ILogger<DiningHomepageLogic> logger;

        public DiningHomepageLogic(DiningHomepageViewModel viewModel) : base(viewModel)
        {
            logger = viewModel.Arguments.LoggerFactory.CreateLogger<DiningHomepageLogic>();
        }

        public async void PageLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.MostEatenDish = await ViewModel.Arguments.StatisticsService.GetMostEatenDish();
                ViewModel.LeastEatenDish = await ViewModel.Arguments.StatisticsService.GetLeastEatenDish();
                ViewModel.UniqueDishesEaten = await ViewModel.Arguments.StatisticsService.GetUniqueDishesEaten();
                ViewModel.MostRecentlyAddedDish =
                    await ViewModel.Arguments.StatisticsService.GetMostRecentlyAddedDish();

                var ingredients = await ViewModel.Arguments.StatisticsService.GetMostUsedIngredients();

                ViewModel.MostUsedIngredients.Clear();

                foreach (IngredientUsageStatistic ingredient in ingredients)
                {
                    ViewModel.MostUsedIngredients.Add(ingredient);
                }
            }
            catch (Exception exe)
            {
                logger.LogError(exe, $"Failed to retrieve Statistics data.");
            }
        }

        public void ImportDataClicked(object sender, RoutedEventArgs e)
        {
            DiningImportPage.DiningImportPageArguments arguments =
                ViewModel.Arguments.ArgumentsFactory.CreateDiningImportPageArguments();

            var page = new DiningImportPage(arguments);

            ViewModel.Arguments.NavigationService.NavigateTo(page, nameof(DiningImportPage));
        }

        public void IngredientsClicked(object sender, RoutedEventArgs e)
        {
        }

        public void DinnersClicked(object sender, RoutedEventArgs e)
        {
        }

        public void DishesClicked(object sender, RoutedEventArgs e)
        {
        }

        public void MoreStatisticsClicked(object sender, RoutedEventArgs e)
        {
        }
    }
}
