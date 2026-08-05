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
        public ObservableCollection<DishEatingStatistic> MostEatenDishes { get; } = [];
        public ObservableCollection<DishEatingStatistic> LeastEatenDishes { get; } = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MostRecentlyAddedDishName))]
        [NotifyPropertyChangedFor(nameof(MostRecentlyAddedDishDetails))]
        private RecentlyAddedDishStatistic? mostRecentlyAddedDish;

        [ObservableProperty] private int uniqueDishesEaten;

        public string MostRecentlyAddedDishName =>
            MostRecentlyAddedDish?.DishName ?? "No dishes registered";

        public string MostRecentlyAddedDishDetails =>
            MostRecentlyAddedDish is null ? string.Empty : $"Added {MostRecentlyAddedDish.CreatedDateTime:dd-MM-yyyy}";
    }
}
