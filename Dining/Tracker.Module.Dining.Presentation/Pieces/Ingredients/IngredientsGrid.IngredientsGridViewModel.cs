using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI.UI.Controls;
using Tracker.Shared.Frontend.Pieces;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Module.Dining.Model.Entity;

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients;

internal sealed partial class IngredientsGrid
{
    internal sealed partial class IngredientsGridViewModel(IngredientsGridArguments arguments) : ObservableObject
    {
        [ObservableProperty] private string nameSearchText = string.Empty;
        public IngredientsGridArguments Arguments { get; } = arguments;

        internal EntityDataGrid<IngredientGridItem, IngredientsGridUi.IngredientGridColumns> EntityDataGrid
        {
            get;
            set;
        } = null!;

        internal DataGrid DataGrid => EntityDataGrid.Api.DataGrid;

        public ComplexSearchableIngredient Searchable { get; } = new();
        public ObservableCollection<IngredientGridItem> IngredientItems { get; } = [];

        public IngredientGridItem? SelectedIngredientItem => EntityDataGrid.Api.SelectedItem;
        public Ingredient? SelectedIngredient => SelectedIngredientItem?.Ingredient;
        public int SelectedIngredientId => EntityDataGrid.Api.SelectedId;

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
    }
}
