using CommunityToolkit.Mvvm.ComponentModel;
using Tracker.Shared.Frontend.Pieces;

namespace Tracker.Module.Dining.Presentation.Pages.Search;

internal sealed partial class DinnerTemplateAdvancedSearchPage
{
    internal sealed partial class DinnerTemplateAdvancedSearchPageViewModel(
        DinnerTemplateAdvancedSearchPageArguments arguments) : ObservableObject
    {
        public DinnerTemplateAdvancedSearchPageArguments Arguments { get; } = arguments;

        internal NullableBooleanOptionBar IsTakeAwayOptionBar { get; set; } = null!;
        internal NullableBooleanOptionBar HasLeftoversOptionBar { get; set; } = null!;
        internal NullableBooleanOptionBar LeftoversEnoughForDinnerOptionBar { get; set; } = null!;
        internal NullableBooleanOptionBar IsLeftoversOptionBar { get; set; } = null!;
        internal NullableBooleanOptionBar IsEatenOutOptionBar { get; set; } = null!;
        internal NullableBooleanOptionBar IsReadyMadeDishOptionBar { get; set; } = null!;

        [ObservableProperty] [NotifyPropertyChangedFor(nameof(SearchModeText))]
        private bool useFuzzySearch = string.IsNullOrWhiteSpace(arguments.Searchable.Searchable.Name);

        [ObservableProperty] private bool? selectedIsTakeAway = arguments.Searchable.IsTakeAway;

        [ObservableProperty] private bool? selectedHasLeftovers = arguments.Searchable.HasLeftovers;

        [ObservableProperty]
        private bool? selectedLeftoversEnoughForDinner = arguments.Searchable.LeftoversEnoughForDinner;

        [ObservableProperty] private bool? selectedIsLeftovers = arguments.Searchable.IsLeftovers;

        [ObservableProperty] private bool? selectedIsEatenOut = arguments.Searchable.IsEatenOut;

        [ObservableProperty] private bool? selectedIsReadyMadeDish = arguments.Searchable.IsReadyMadeDish;

        public string SearchModeText =>
            UseFuzzySearch ? "Fuzzy search" : "Exact search";

        public void ConnectIsTakeAwayOptionBar(NullableBooleanOptionBar optionBar)
        {
            IsTakeAwayOptionBar = optionBar;
            IsTakeAwayOptionBar.ViewModel.SelectionChanged += IsTakeAwaySelectionChanged;
        }

        private void IsTakeAwaySelectionChanged(object? sender, EventArgs e)
        {
            SelectedIsTakeAway = IsTakeAwayOptionBar.ViewModel.SelectedValue;
        }

        public void ConnectHasLeftoversOptionBar(NullableBooleanOptionBar optionBar)
        {
            HasLeftoversOptionBar = optionBar;
            HasLeftoversOptionBar.ViewModel.SelectionChanged += HasLeftoversSelectionChanged;
        }

        private void HasLeftoversSelectionChanged(object? sender, EventArgs e)
        {
            SelectedHasLeftovers = HasLeftoversOptionBar.ViewModel.SelectedValue;
        }

        public void ConnectLeftoversEnoughForDinnerOptionBar(NullableBooleanOptionBar optionBar)
        {
            LeftoversEnoughForDinnerOptionBar = optionBar;

            LeftoversEnoughForDinnerOptionBar.ViewModel.SelectionChanged += LeftoversEnoughForDinnerSelectionChanged;
        }

        private void LeftoversEnoughForDinnerSelectionChanged(object? sender, EventArgs e)
        {
            SelectedLeftoversEnoughForDinner = LeftoversEnoughForDinnerOptionBar.ViewModel.SelectedValue;
        }

        public void ConnectIsLeftoversOptionBar(NullableBooleanOptionBar optionBar)
        {
            IsLeftoversOptionBar = optionBar;
            IsLeftoversOptionBar.ViewModel.SelectionChanged += IsLeftoversSelectionChanged;
        }

        private void IsLeftoversSelectionChanged(object? sender, EventArgs e)
        {
            SelectedIsLeftovers = IsLeftoversOptionBar.ViewModel.SelectedValue;
        }

        public void ConnectIsEatenOutOptionBar(NullableBooleanOptionBar optionBar)
        {
            IsEatenOutOptionBar = optionBar;
            IsEatenOutOptionBar.ViewModel.SelectionChanged += IsEatenOutSelectionChanged;
        }

        private void IsEatenOutSelectionChanged(object? sender, EventArgs e)
        {
            SelectedIsEatenOut = IsEatenOutOptionBar.ViewModel.SelectedValue;
        }

        public void ConnectIsReadyMadeDishOptionBar(NullableBooleanOptionBar optionBar)
        {
            IsReadyMadeDishOptionBar = optionBar;

            IsReadyMadeDishOptionBar.ViewModel.SelectionChanged += IsReadyMadeDishSelectionChanged;
        }

        private void IsReadyMadeDishSelectionChanged(object? sender, EventArgs e)
        {
            SelectedIsReadyMadeDish = IsReadyMadeDishOptionBar.ViewModel.SelectedValue;
        }
    }
}
