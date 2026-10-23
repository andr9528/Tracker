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

        [ObservableProperty] private DinnerTemplate? selectedDinnerTemplate;

        [ObservableProperty] private int selectedDinnerTemplateId = arguments.SelectedDinnerTemplateId;
        public DinnerTemplatesGridArguments Arguments { get; } = arguments;

        internal EntityDataGrid<DinnerTemplate, DinnerTemplatesGridUi.DinnerTemplateGridColumns> EntityDataGrid
        {
            get;
            set;
        } = null!;

        internal DataGrid DataGrid => EntityDataGrid.Api.DataGrid;

        public ComplexSearchableDinnerTemplate Searchable { get; } = new();

        public ObservableCollection<DinnerTemplate> DinnerTemplates { get; } = [];

        public event EventHandler? SearchChanged;
        public event EventHandler? DinnerTemplateSelectionChanged;

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

        partial void OnSelectedDinnerTemplateIdChanged(int value)
        {
            DinnerTemplate? template = DinnerTemplates.FirstOrDefault(x => x.Id == value);

            if (SelectedDinnerTemplate?.Id != template?.Id)
            {
                SelectedDinnerTemplate = template;
            }
        }

        partial void OnSelectedDinnerTemplateChanged(DinnerTemplate? value)
        {
            int templateId = value?.Id ?? 0;

            if (SelectedDinnerTemplateId != templateId)
            {
                SelectedDinnerTemplateId = templateId;
            }

            DinnerTemplateSelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
