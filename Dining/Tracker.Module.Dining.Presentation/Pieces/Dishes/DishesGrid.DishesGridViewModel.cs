using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI.UI.Controls;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Module.Dining.Model.Entity;

namespace Tracker.Module.Dining.Presentation.Pieces.Dishes;

internal sealed partial class DishesGrid
{
    internal sealed partial class DishesGridViewModel(DishesGridArguments arguments) : ObservableObject
    {
        [ObservableProperty] private string ingredientSearchText = string.Empty;

        [ObservableProperty] private string nameSearchText = string.Empty;

        [ObservableProperty] private int selectedDishId = arguments.SelectedDishId;

        [ObservableProperty] private DishGridItem? selectedDishItem;
        public DishesGridArguments Arguments { get; } = arguments;

        internal DataGrid DataGrid { get; set; } = null!;

        public ComplexSearchableDish Searchable { get; } = new();

        public ObservableCollection<DishGridItem> DishItems { get; } = [];

        public Dish? SelectedDish => SelectedDishItem?.Dish;

        public event EventHandler? SearchChanged;
        public event EventHandler? DishSelectionChanged;

        partial void OnNameSearchTextChanged(string value)
        {
            UpdateNameSearch(value);
        }

        private void UpdateNameSearch(string? name)
        {
            if (UsesFuzzySearch())
            {
                Searchable.Name = name;
                Searchable.Searchable.Name = string.Empty;
            }
            else
            {
                Searchable.Name = null;
                Searchable.Searchable.Name = name ?? string.Empty;
            }

            SearchChanged?.Invoke(this, EventArgs.Empty);
        }

        private bool UsesFuzzySearch()
        {
            return string.IsNullOrWhiteSpace(Searchable.Searchable.Name);
        }

        partial void OnSelectedDishIdChanged(int value)
        {
            DishGridItem? item = DishItems.FirstOrDefault(x => x.Id == value);

            if (SelectedDishItem?.Id != item?.Id)
            {
                SelectedDishItem = item;
            }

            DishSelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        partial void OnSelectedDishItemChanged(DishGridItem? value)
        {
            int dishId = value?.Id ?? 0;

            if (SelectedDishId == dishId)
            {
                return;
            }

            SelectedDishId = dishId;
        }

        partial void OnIngredientSearchTextChanged(string value)
        {
            UpdateIngredientSearch(value);
        }

        private void UpdateIngredientSearch(string? ingredients)
        {
            Searchable.Ingredients = ingredients;

            SearchChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
