using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Presentation.Pages.Search;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Extensions;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Dinners;

internal sealed partial class DinnerTemplatesGrid
{
    internal sealed class DinnerTemplatesGridLogic : BaseLogic<DinnerTemplatesGridViewModel>, IDinnerTemplatesGridApi
    {
        private readonly IEntityQueryService<DinnerTemplate, SearchableDinnerTemplate> queryService;
        private readonly IUiDispatcher uiDispatcher;
        private readonly ILogger<DinnerTemplatesGridLogic> logger;

        public DinnerTemplatesGridLogic(DinnerTemplatesGridViewModel viewModel) : base(viewModel)
        {
            queryService = ViewModel.Arguments.QueryService;
            uiDispatcher = ViewModel.Arguments.UiDispatcher;
            logger = ViewModel.Arguments.LoggerFactory.CreateLogger<DinnerTemplatesGridLogic>();

            ViewModel.SearchChanged += SearchChanged;
        }

        private async void SearchChanged(object? sender, EventArgs e)
        {
            try
            {
                await RefreshDinnerTemplates();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Exception caught during refresh of Dinner Templates");
            }
        }

        internal async Task RefreshDinnerTemplates()
        {
            RememberSelectedDinnerTemplate();

            var templates = (await queryService.GetEntitiesComplex(ViewModel.Searchable)).ToList();

            templates = ViewModel.DataGrid.ApplyCurrentSort(templates).ToList();

            logger.LogInformation("Dinner Template search returned {DinnerTemplateCount} templates.", templates.Count);

            uiDispatcher.TryEnqueue(() =>
            {
                logger.LogDebug("Updating Dinner Templates collection. Existing count: {ExistingCount}",
                    ViewModel.DinnerTemplates.Count);

                ViewModel.DinnerTemplates.ReplaceItems(templates);
                ViewModel.DataGrid.Refresh();

                RestoreSelectedDinnerTemplate();

                logger.LogDebug(
                    "Dinner Templates collection updated. New count: {NewCount}, SelectedDinnerTemplateId: {SelectedDinnerTemplateId}, SelectedDinnerTemplate: '{SelectedDinnerTemplateName}'",
                    ViewModel.DinnerTemplates.Count, ViewModel.SelectedDinnerTemplateId,
                    ViewModel.SelectedDinnerTemplate?.Name);
            });
        }

        private void RememberSelectedDinnerTemplate()
        {
            if (ViewModel.SelectedDinnerTemplate is null)
            {
                return;
            }

            ViewModel.SelectedDinnerTemplateId = ViewModel.SelectedDinnerTemplate.Id;
        }

        private void RestoreSelectedDinnerTemplate()
        {
            ViewModel.SelectedDinnerTemplate =
                ViewModel.DinnerTemplates.FirstOrDefault(x => x.Id == ViewModel.SelectedDinnerTemplateId);
        }

        internal void AdvancedSearchClicked(object? sender, EventArgs e)
        {
            DinnerTemplateAdvancedSearchPage.DinnerTemplateAdvancedSearchPageArguments arguments =
                ViewModel.Arguments.ArgumentsFactory.CreateDinnerTemplateAdvancedSearchPageArguments(ViewModel
                    .Searchable);

            var page = new DinnerTemplateAdvancedSearchPage(arguments);

            arguments.NavigationService.NavigateTo(page, nameof(DinnerTemplateAdvancedSearchPage));
        }

        #region Implementation of IDinnerTemplatesGridApi

        /// <inheritdoc />
        public DinnerTemplate? SelectedDinnerTemplate =>
            ViewModel.SelectedDinnerTemplate;

        /// <inheritdoc />
        public Task Refresh()
        {
            return RefreshDinnerTemplates();
        }

        #endregion
    }
}
