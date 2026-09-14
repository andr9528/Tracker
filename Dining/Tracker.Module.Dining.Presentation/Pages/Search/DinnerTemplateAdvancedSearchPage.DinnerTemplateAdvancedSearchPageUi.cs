using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;
using Tracker.Shared.Frontend.Pieces;

namespace Tracker.Module.Dining.Presentation.Pages.Search;

internal sealed partial class DinnerTemplateAdvancedSearchPage
{
    internal sealed class DinnerTemplateAdvancedSearchPageUi(
        DinnerTemplateAdvancedSearchPageLogic logic,
        DinnerTemplateAdvancedSearchPageViewModel viewModel)
        : BaseUi<DinnerTemplateAdvancedSearchPageLogic, DinnerTemplateAdvancedSearchPageViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            grid.Padding = new Thickness(16);
            grid.RowSpacing = 12;
            grid.ColumnSpacing = 8;

            grid.DefineRows(GridLength.Auto, GridLength.Auto, GridLength.Auto, GridLength.Auto, GridLength.Auto,
                new GridLength(1, GridUnitType.Star), GridLength.Auto);

            grid.DefineColumns(new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star));
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(CreateHeader().SetRow(0).SetColumn(0, 2));
            grid.Children.Add(CreateFuzzySearchGrid().SetRow(1).SetColumn(0, 2));

            grid.Children.Add(CreateIsTakeAwayOptionBar().SetRow(2).SetColumn(0));
            grid.Children.Add(CreateHasLeftoversOptionBar().SetRow(2).SetColumn(1));

            grid.Children.Add(CreateIsEatenOutOptionBar().SetRow(3).SetColumn(0));
            grid.Children.Add(CreateLeftoversEnoughForDinnerOptionBar().SetRow(3).SetColumn(1));

            grid.Children.Add(CreateIsReadyMadeDishOptionBar().SetRow(4).SetColumn(0));
            grid.Children.Add(CreateIsLeftoversOptionBar().SetRow(4).SetColumn(1));

            grid.Children.Add(CreateActionButtonGrid().SetRow(6).SetColumn(0, 2));
        }

        private TextBlock CreateHeader()
        {
            return TextBlockFactory.CreateHeader("Advanced Dinner Template Search");
        }

        private Grid CreateFuzzySearchGrid()
        {
            return SimplePieceFactory.CreateFuzzySearchGrid(
                nameof(DinnerTemplateAdvancedSearchPageViewModel.UseFuzzySearch),
                nameof(DinnerTemplateAdvancedSearchPageViewModel.SearchModeText), out CheckBox _);
        }

        private NullableBooleanOptionBar CreateIsTakeAwayOptionBar()
        {
            NullableBooleanOptionBar optionBar = CreateBooleanOptionBar("Take Away", ViewModel.SelectedIsTakeAway);

            ViewModel.ConnectIsTakeAwayOptionBar(optionBar);

            return optionBar;
        }

        private NullableBooleanOptionBar CreateHasLeftoversOptionBar()
        {
            NullableBooleanOptionBar optionBar = CreateBooleanOptionBar(
                "Has Leftovers", ViewModel.SelectedHasLeftovers);

            ViewModel.ConnectHasLeftoversOptionBar(optionBar);

            return optionBar;
        }

        private NullableBooleanOptionBar CreateLeftoversEnoughForDinnerOptionBar()
        {
            NullableBooleanOptionBar optionBar = CreateBooleanOptionBar(
                "Enough For Dinner", ViewModel.SelectedLeftoversEnoughForDinner);

            ViewModel.ConnectLeftoversEnoughForDinnerOptionBar(optionBar);

            return optionBar;
        }

        private NullableBooleanOptionBar CreateIsLeftoversOptionBar()
        {
            NullableBooleanOptionBar optionBar = CreateBooleanOptionBar("Is Leftovers", ViewModel.SelectedIsLeftovers);

            ViewModel.ConnectIsLeftoversOptionBar(optionBar);

            return optionBar;
        }

        private NullableBooleanOptionBar CreateIsEatenOutOptionBar()
        {
            NullableBooleanOptionBar optionBar = CreateBooleanOptionBar("Eaten Out", ViewModel.SelectedIsEatenOut);

            ViewModel.ConnectIsEatenOutOptionBar(optionBar);

            return optionBar;
        }

        private NullableBooleanOptionBar CreateIsReadyMadeDishOptionBar()
        {
            NullableBooleanOptionBar optionBar = CreateBooleanOptionBar(
                "Ready Made Dish", ViewModel.SelectedIsReadyMadeDish);

            ViewModel.ConnectIsReadyMadeDishOptionBar(optionBar);

            return optionBar;
        }

        private NullableBooleanOptionBar CreateBooleanOptionBar(string title, bool? selectedValue)
        {
            NullableBooleanOptionBar.NullableBooleanOptionBarArguments arguments =
                ViewModel.Arguments.ArgumentsFactory.CreateNullableBooleanOptionBarArguments(title, selectedValue);

            return new NullableBooleanOptionBar(arguments);
        }

        private Grid CreateActionButtonGrid()
        {
            return SimplePieceFactory.CreateThreeButtonGrid("Reset", Logic.ResetClicked, "Cancel", Logic.CancelClicked,
                "Apply", Logic.ApplyClicked);
        }
    }
}
