using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Tracker.Module.Dining.Abstraction.Records;

namespace Tracker.Module.Dining.Presentation.Pages;

internal sealed partial class DiningImportPage
{
    internal sealed partial class DiningImportPageViewModel(DiningImportPageArguments arguments) : ObservableObject
    {
        public DiningImportPageArguments Arguments { get; } = arguments;

        public ObservableCollection<string> StatusMessages { get; } =
        [
            "No file selected.",
        ];

        [ObservableProperty] [NotifyPropertyChangedFor(nameof(CanImport))]
        private string selectedFilePath = string.Empty;

        [ObservableProperty] [NotifyPropertyChangedFor(nameof(CanImport))]
        private bool isImporting;

        [ObservableProperty] private ImportResult? result;

        public bool CanImport =>
            !IsImporting && !string.IsNullOrWhiteSpace(SelectedFilePath);
    }
}
