using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Tracker.Module.Dining.Abstraction.Records;

namespace Tracker.Module.Dining.Presentation.Pages;

internal sealed partial class DiningHomepage
{
    internal sealed partial class DiningHomepageViewModel(DiningHomepageArguments arguments) : ObservableObject
    {
        public DiningHomepageArguments Arguments { get; } = arguments;

        public ObservableCollection<IngredientUsageStatistic> MostUsedIngredients { get; } = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MostEatenDishName))]
        [NotifyPropertyChangedFor(nameof(MostEatenDishDetails))]
        private DishEatingStatistic? mostEatenDish;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LeastEatenDishName))]
        [NotifyPropertyChangedFor(nameof(LeastEatenDishDetails))]
        private DishEatingStatistic? leastEatenDish;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MostRecentlyAddedDishName))]
        [NotifyPropertyChangedFor(nameof(MostRecentlyAddedDishDetails))]
        private RecentlyAddedDishStatistic? mostRecentlyAddedDish;

        [ObservableProperty] private int uniqueDishesEaten;

        public string MostEatenDishName =>
            MostEatenDish?.DishName ?? "No dining data available";

        public string MostEatenDishDetails =>
            CreateDishDetails(MostEatenDish);

        public string LeastEatenDishName =>
            LeastEatenDish?.DishName ?? "No dining data available";

        public string LeastEatenDishDetails =>
            CreateDishDetails(LeastEatenDish);

        public string MostRecentlyAddedDishName =>
            MostRecentlyAddedDish?.DishName ?? "No dishes registered";

        public string MostRecentlyAddedDishDetails =>
            MostRecentlyAddedDish is null ? string.Empty : $"Added {MostRecentlyAddedDish.CreatedDateTime:dd-MM-yyyy}";

        private static string CreateDishDetails(DishEatingStatistic? statistic)
        {
            if (statistic is null)
            {
                return string.Empty;
            }

            string dinnerText = statistic.DinnerCount == 1 ? "1 dinner" : $"{statistic.DinnerCount} dinners";

            return $"{dinnerText} · Last eaten {statistic.LastEaten:dd-MM-yyyy}";
        }
    }
}
