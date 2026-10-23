using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI.UI.Controls;
using Tracker.Shared.Frontend.Pieces;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Module.Dining.Model.Entity;

namespace Tracker.Module.Dining.Presentation.Pieces.Dishes;

internal sealed partial class DishesGrid
{
    internal sealed partial class DishesGridViewModel(DishesGridArguments arguments) : ObservableObject
    {
        [ObservableProperty] private string ingredientSearchText = string.Empty;

        [ObservableProperty] private string nameSearchText = string.Empty;
        public DishesGridArguments Arguments { get; } = arguments;

        internal EntityDataGrid<DishGridItem, DishesGridUi.DishGridColumns> EntityDataGrid { get; set; } = null!;

        internal DataGrid DataGrid => EntityDataGrid.Api.DataGrid;

        public ComplexSearchableDish Searchable { get; } = new();

        public ObservableCollection<DishGridItem> DishItems { get; } = [];

        public DishGridItem? SelectedDishItem => EntityDataGrid.Api.SelectedItem;
        public Dish? SelectedDish => SelectedDishItem?.Dish;
        public int SelectedDishId => EntityDataGrid.Api.SelectedId;

        public event EventHandler? SearchChanged;

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
