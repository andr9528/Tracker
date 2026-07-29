using CommunityToolkit.Mvvm.ComponentModel;
using Tracker.Shared.Frontend.Pieces;

namespace Tracker.Module.Dining.Presentation.Pages.Search;

internal sealed partial class IngredientAdvancedSearchPage
{
    internal sealed partial class IngredientAdvancedSearchPageViewModel(
        IngredientAdvancedSearchPageArguments arguments) : ObservableObject
    {
        public IngredientAdvancedSearchPageArguments Arguments { get; } = arguments;

        internal NullableBooleanOptionBar InStockOptionBar { get; set; } = null!;

        [ObservableProperty] private bool? selectedInStock = arguments.Searchable.InStock;

        [ObservableProperty] [NotifyPropertyChangedFor(nameof(SearchModeText))]
        private bool useFuzzySearch = string.IsNullOrWhiteSpace(arguments.Searchable.Searchable.Name);

        [ObservableProperty] private string minimumDishCountText =
            arguments.Searchable.MinimumDishCount?.ToString() ?? string.Empty;

        [ObservableProperty] private string maximumDishCountText =
            arguments.Searchable.MaximumDishCount?.ToString() ?? string.Empty;

        [ObservableProperty] private string? validationMessage;

        public string SearchModeText =>
            UseFuzzySearch ? "Fuzzy search" : "Exact search";

        public bool TryGetDishCountRange(out int? minimum, out int? maximum)
        {
            minimum = null;
            maximum = null;
            ValidationMessage = null;

            if (!TryParseNullableNonNegative(MinimumDishCountText, "Minimum dish count", out minimum) ||
                !TryParseNullableNonNegative(MaximumDishCountText, "Maximum dish count", out maximum))
            {
                return false;
            }

            if (minimum > maximum)
            {
                ValidationMessage = "Minimum dish count cannot be greater than maximum dish count.";
                return false;
            }

            return true;
        }

        public void ConnectInStockOptionBar(NullableBooleanOptionBar optionBar)
        {
            InStockOptionBar = optionBar;

            InStockOptionBar.ViewModel.SelectionChanged += InStockSelectionChanged;
        }

        private void InStockSelectionChanged(object? sender, EventArgs e)
        {
            SelectedInStock = InStockOptionBar.ViewModel.SelectedValue;
        }

        private bool TryParseNullableNonNegative(string? value, string fieldName, out int? parsed)
        {
            parsed = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            if (!int.TryParse(value, out int result) || result < 0)
            {
                ValidationMessage = $"{fieldName} must be a non-negative whole number.";
                return false;
            }

            parsed = result;
            return true;
        }
    }
}
