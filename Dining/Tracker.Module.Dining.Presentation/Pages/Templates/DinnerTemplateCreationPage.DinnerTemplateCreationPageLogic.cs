using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Presentation.Pieces.Dinners;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Core;

namespace Tracker.Module.Dining.Presentation.Pages.Templates;

internal sealed partial class DinnerTemplateCreationPage
{
    private sealed class DinnerTemplateCreationPageLogic : BaseLogic<DinnerTemplateCreationPageViewModel>
    {
        private readonly IEntityQueryService<DinnerTemplate, SearchableDinnerTemplate> queryService;
        private readonly INavigationService navigationService;
        private readonly ILogger<DinnerTemplateCreationPageLogic> logger;

        public DinnerTemplateCreationPageLogic(DinnerTemplateCreationPageViewModel viewModel) : base(viewModel)
        {
            queryService = ViewModel.Arguments.DinnerTemplateQueryService;
            navigationService = ViewModel.Arguments.NavigationService;
            logger = ViewModel.Arguments.LoggerFactory.CreateLogger<DinnerTemplateCreationPageLogic>();
        }

        internal async Task SaveClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsUserInputValid())
                {
                    logger.LogInformation("Dinner template creation was blocked by invalid input.");

                    return;
                }

                DinnerTemplate dinnerTemplate = BuildNewDinnerTemplate();

                await queryService.AddEntity(dinnerTemplate);

                logger.LogInformation("Created dinner template {DinnerTemplateId} with name '{DinnerTemplateName}'.",
                    dinnerTemplate.Id, dinnerTemplate.Name);

                DinnerTemplateDetailsPage.DinnerTemplateDetailsPageArguments arguments =
                    ViewModel.Arguments.ArgumentsFactory.CreateDinnerTemplateDetailsPageArguments(dinnerTemplate.Id);

                var details = new DinnerTemplateDetailsPage(arguments);

                navigationService.NavigateTo(details, nameof(DinnerTemplateDetailsPage));
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Caught exception while trying to create a new Dinner Template.");
            }
        }

        private bool IsUserInputValid()
        {
            return !string.IsNullOrWhiteSpace(ViewModel.DinnerTemplateEditor.Api.Name);
        }

        private DinnerTemplate BuildNewDinnerTemplate()
        {
            DinnerTemplateEditor.IDinnerTemplateEditorApi editor = ViewModel.DinnerTemplateEditor.Api;

            return new DinnerTemplate
            {
                Name = editor.Name,
                IsTakeAway = editor.IsTakeAway,
                HasLeftovers = editor.HasLeftovers,
                LeftoversEnoughForDinner = editor.LeftoversEnoughForDinner,
                IsLeftovers = editor.IsLeftovers,
                IsEatenOut = editor.IsEatenOut,
                IsReadyMadeDish = editor.IsReadyMadeDish,
            };
        }

        internal void CancelClicked(object sender, RoutedEventArgs e)
        {
            navigationService.NavigateBack();
        }
    }
}
