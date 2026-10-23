using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI.UI.Controls;
using Tracker.Shared.Frontend.Pieces;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Module.Dining.Model.Entity;

namespace Tracker.Module.Dining.Presentation.Pieces.Dinners;

internal sealed partial class DinnerTemplatesGrid
{
    internal sealed partial class DinnerTemplatesGridViewModel(DinnerTemplatesGridArguments arguments)
        : ObservableObject
    {
        [ObservableProperty] private string nameSearchText = string.Empty;
        public DinnerTemplatesGridArguments Arguments { get; } = arguments;

        internal EntityDataGrid<DinnerTemplate, DinnerTemplatesGridUi.DinnerTemplateGridColumns> EntityDataGrid
        {
            get;
            set;
        } = null!;

        internal DataGrid DataGrid => EntityDataGrid.Api.DataGrid;

        public ComplexSearchableDinnerTemplate Searchable { get; } = new();

        public ObservableCollection<DinnerTemplate> DinnerTemplates { get; } = [];

        public DinnerTemplate? SelectedDinnerTemplate => EntityDataGrid.Api.SelectedItem;
        public int SelectedDinnerTemplateId => EntityDataGrid.Api.SelectedId;

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
