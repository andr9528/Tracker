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
                var mostEatenDishes = await ViewModel.Arguments.StatisticsService.GetMostEatenDishes();

                var leastEatenDishes = await ViewModel.Arguments.StatisticsService.GetLeastEatenDishes();

                ViewModel.UniqueDishesEaten = await ViewModel.Arguments.StatisticsService.GetUniqueDishesEaten();

                ViewModel.MostRecentlyAddedDish =
                    await ViewModel.Arguments.StatisticsService.GetMostRecentlyAddedDish();

                var ingredients = await ViewModel.Arguments.StatisticsService.GetMostUsedIngredients();

                ReplaceCollection(ViewModel.MostEatenDishes, mostEatenDishes);
                ReplaceCollection(ViewModel.LeastEatenDishes, leastEatenDishes);
                ReplaceCollection(ViewModel.MostUsedIngredients, ingredients);
            }
            catch (Exception exe)
            {
                logger.LogError(exe, "Failed to retrieve Statistics data.");
            }
        }

        private void ReplaceCollection<T>(ICollection<T> destination, IEnumerable<T> source)
        {
            destination.Clear();

            foreach (T item in source)
            {
                destination.Add(item);
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
