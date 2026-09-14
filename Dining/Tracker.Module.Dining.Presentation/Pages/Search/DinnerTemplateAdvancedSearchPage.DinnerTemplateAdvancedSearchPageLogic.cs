using Microsoft.Extensions.Logging;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Pieces;

namespace Tracker.Module.Dining.Presentation.Pages.Search;

internal sealed partial class DinnerTemplateAdvancedSearchPage
{
    internal sealed class DinnerTemplateAdvancedSearchPageLogic : BaseLogic<DinnerTemplateAdvancedSearchPageViewModel>
    {
        private readonly ILogger<DinnerTemplateAdvancedSearchPageLogic> logger;

        public DinnerTemplateAdvancedSearchPageLogic(DinnerTemplateAdvancedSearchPageViewModel viewModel) :
            base(viewModel)
        {
            logger = ViewModel.Arguments.LoggerFactory.CreateLogger<DinnerTemplateAdvancedSearchPageLogic>();
        }

        private void ApplyNameSearchMode()
        {
            string name = ViewModel.Arguments.Searchable.Name ??
                          ViewModel.Arguments.Searchable.Searchable.Name ?? string.Empty;

            if (ViewModel.UseFuzzySearch)
            {
                ViewModel.Arguments.Searchable.Name = name;
                ViewModel.Arguments.Searchable.Searchable.Name = string.Empty;
            }
            else
            {
                ViewModel.Arguments.Searchable.Name = null;
                ViewModel.Arguments.Searchable.Searchable.Name = name;
            }
        }

        public void ResetClicked(object sender, RoutedEventArgs e)
        {
            ViewModel.UseFuzzySearch = true;

            ResetBooleanOptionBars();

            ViewModel.Arguments.Searchable.IsTakeAway = null;
            ViewModel.Arguments.Searchable.HasLeftovers = null;
            ViewModel.Arguments.Searchable.LeftoversEnoughForDinner = null;
            ViewModel.Arguments.Searchable.IsLeftovers = null;
            ViewModel.Arguments.Searchable.IsEatenOut = null;
            ViewModel.Arguments.Searchable.IsReadyMadeDish = null;

            ApplyNameSearchMode();
        }

        private void ResetBooleanOptionBars()
        {
            ViewModel.SelectedIsTakeAway = null;
            ViewModel.SelectedHasLeftovers = null;
            ViewModel.SelectedLeftoversEnoughForDinner = null;
            ViewModel.SelectedIsLeftovers = null;
            ViewModel.SelectedIsEatenOut = null;
            ViewModel.SelectedIsReadyMadeDish = null;

            SetEitherSelected(ViewModel.IsTakeAwayOptionBar);
            SetEitherSelected(ViewModel.HasLeftoversOptionBar);
            SetEitherSelected(ViewModel.LeftoversEnoughForDinnerOptionBar);
            SetEitherSelected(ViewModel.IsLeftoversOptionBar);
            SetEitherSelected(ViewModel.IsEatenOutOptionBar);
            SetEitherSelected(ViewModel.IsReadyMadeDishOptionBar);
        }

        private void SetEitherSelected(NullableBooleanOptionBar optionBar)
        {
            optionBar.ViewModel.SelectedValue = null;
            optionBar.ViewModel.EitherButton.IsChecked = true;
        }

        public void CancelClicked(object sender, RoutedEventArgs e)
        {
            ViewModel.Arguments.NavigationService.NavigateBack();
        }

        public void ApplyClicked(object sender, RoutedEventArgs e)
        {
            ApplyNameSearchMode();

            ViewModel.Arguments.Searchable.IsTakeAway = ViewModel.SelectedIsTakeAway;

            ViewModel.Arguments.Searchable.HasLeftovers = ViewModel.SelectedHasLeftovers;

            ViewModel.Arguments.Searchable.LeftoversEnoughForDinner = ViewModel.SelectedLeftoversEnoughForDinner;

            ViewModel.Arguments.Searchable.IsLeftovers = ViewModel.SelectedIsLeftovers;

            ViewModel.Arguments.Searchable.IsEatenOut = ViewModel.SelectedIsEatenOut;

            ViewModel.Arguments.Searchable.IsReadyMadeDish = ViewModel.SelectedIsReadyMadeDish;

            logger.LogInformation(
                "Applied advanced Dinner Template search. Fuzzy: {UseFuzzySearch}, " +
                "IsTakeAway: {IsTakeAway}, HasLeftovers: {HasLeftovers}, " +
                "LeftoversEnoughForDinner: {LeftoversEnoughForDinner}, " +
                "IsLeftovers: {IsLeftovers}, IsEatenOut: {IsEatenOut}, " + "IsReadyMadeDish: {IsReadyMadeDish}",
                ViewModel.UseFuzzySearch, ViewModel.SelectedIsTakeAway, ViewModel.SelectedHasLeftovers,
                ViewModel.SelectedLeftoversEnoughForDinner, ViewModel.SelectedIsLeftovers, ViewModel.SelectedIsEatenOut,
                ViewModel.SelectedIsReadyMadeDish);

            ViewModel.Arguments.NavigationService.NavigateBack();
        }
    }
}
