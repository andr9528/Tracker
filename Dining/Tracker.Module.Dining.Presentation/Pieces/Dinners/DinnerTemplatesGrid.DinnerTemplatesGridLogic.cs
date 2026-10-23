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
        private readonly ILogger<DinnerTemplatesGridLogic> logger;
        private readonly IEntityQueryService<DinnerTemplate, SearchableDinnerTemplate> queryService;

        public DinnerTemplatesGridLogic(DinnerTemplatesGridViewModel viewModel) : base(viewModel)
        {
            queryService = ViewModel.Arguments.QueryService;
            logger = ViewModel.Arguments.LoggerFactory.CreateLogger<DinnerTemplatesGridLogic>();

            ViewModel.SearchChanged += SearchChanged;
        }

        #region Implementation of IDinnerTemplatesGridApi

        /// <inheritdoc />
        public Task Refresh()
        {
            return RefreshDinnerTemplates();
        }

        #endregion

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

        internal Task RefreshDinnerTemplates()
        {
            return ViewModel.EntityDataGrid.Api.Refresh();
        }

        internal async Task<IEnumerable<DinnerTemplate>> GetItems()
        {
            var templates = (await queryService.GetEntitiesComplex(ViewModel.Searchable)).ToList();
            var items = templates;
            logger.LogInformation("DinnerTemplates search returned {Count} items.", items.Count);
            return items;
        }


        internal void AdvancedSearchClicked(object? sender, EventArgs e)
        {
            DinnerTemplateAdvancedSearchPage.DinnerTemplateAdvancedSearchPageArguments arguments =
                ViewModel.Arguments.ArgumentsFactory.CreateDinnerTemplateAdvancedSearchPageArguments(ViewModel
                    .Searchable);

            var page = new DinnerTemplateAdvancedSearchPage(arguments);

            arguments.NavigationService.NavigateTo(page, nameof(DinnerTemplateAdvancedSearchPage));
        }

        #region Implementation of IEntitySelectionGridApi<DinnerTemplate>

        /// <inheritdoc />
        public DinnerTemplate? SelectedEntity => ViewModel.SelectedDinnerTemplate;

        /// <inheritdoc />
        public event EventHandler? SelectionChanged
        {
            add => ViewModel.EntityDataGrid.Api.SelectionChanged += value;
            remove => ViewModel.EntityDataGrid.Api.SelectionChanged -= value;
        }

        #endregion
    }
}
