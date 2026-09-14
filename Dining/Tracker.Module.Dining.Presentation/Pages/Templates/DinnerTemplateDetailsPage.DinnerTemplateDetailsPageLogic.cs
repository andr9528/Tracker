using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Presentation.Pieces.Dinners;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Core.Details;

namespace Tracker.Module.Dining.Presentation.Pages.Templates;

internal sealed partial class DinnerTemplateDetailsPage
{
    internal sealed class DinnerTemplateDetailsPageLogic : BaseDetailsPageLogic<DinnerTemplateDetailsPageViewModel>
    {
        private readonly IEntityQueryService<DinnerTemplate, SearchableDinnerTemplate> queryService;
        private readonly IUiDispatcher uiDispatcher;
        private readonly ILogger<DinnerTemplateDetailsPageLogic> logger;

        public DinnerTemplateDetailsPageLogic(DinnerTemplateDetailsPageViewModel viewModel) : base(viewModel)
        {
            queryService = ViewModel.Arguments.DinnerTemplateQueryService;
            uiDispatcher = ViewModel.Arguments.UiDispatcher;
            logger = ViewModel.Arguments.LoggerFactory.CreateLogger<DinnerTemplateDetailsPageLogic>();
        }

        internal async Task RefreshDinnerTemplate()
        {
            DinnerTemplate? dinnerTemplate = await queryService.GetEntity(CreateSearchableDinnerTemplate());

            if (dinnerTemplate is null)
            {
                return;
            }

            uiDispatcher.TryEnqueue(() =>
            {
                ViewModel.DinnerTemplate = dinnerTemplate;

                ApplyDinnerTemplateToEditor();
                UpdateHasChanges();
            });
        }

        private SearchableDinnerTemplate CreateSearchableDinnerTemplate()
        {
            return new SearchableDinnerTemplate
            {
                Id = ViewModel.Arguments.DinnerTemplateId,
            };
        }

        internal void RegisterDinnerTemplateEditorEvents()
        {
            DinnerTemplateEditor.IDinnerTemplateEditorApi editor = ViewModel.DinnerTemplateEditor.Api;

            editor.NameChanged += DinnerTemplateEditorChanged;
            editor.IsTakeAwayChanged += DinnerTemplateEditorChanged;
            editor.HasLeftoversChanged += DinnerTemplateEditorChanged;
            editor.LeftoversEnoughForDinnerChanged += DinnerTemplateEditorChanged;
            editor.IsLeftoversChanged += DinnerTemplateEditorChanged;
            editor.IsEatenOutChanged += DinnerTemplateEditorChanged;
            editor.IsReadyMadeDishChanged += DinnerTemplateEditorChanged;
        }

        private void DinnerTemplateEditorChanged(object? sender, EventArgs e)
        {
            UpdateHasChanges();
        }

        private void ApplyEditorValuesToDinnerTemplate()
        {
            DinnerTemplateEditor.IDinnerTemplateEditorApi editor = ViewModel.DinnerTemplateEditor.Api;

            logger.LogDebug("Applying dinner template changes. DinnerTemplateId={DinnerTemplateId}",
                ViewModel.DinnerTemplate.Id);

            logger.LogDebug("Name: '{OldValue}' -> '{NewValue}'", ViewModel.DinnerTemplate.Name, editor.Name);

            logger.LogDebug("IsTakeAway: '{OldValue}' -> '{NewValue}'", ViewModel.DinnerTemplate.IsTakeAway,
                editor.IsTakeAway);

            logger.LogDebug("HasLeftovers: '{OldValue}' -> '{NewValue}'", ViewModel.DinnerTemplate.HasLeftovers,
                editor.HasLeftovers);

            logger.LogDebug("LeftoversEnoughForDinner: '{OldValue}' -> '{NewValue}'",
                ViewModel.DinnerTemplate.LeftoversEnoughForDinner, editor.LeftoversEnoughForDinner);

            logger.LogDebug("IsLeftovers: '{OldValue}' -> '{NewValue}'", ViewModel.DinnerTemplate.IsLeftovers,
                editor.IsLeftovers);

            logger.LogDebug("IsEatenOut: '{OldValue}' -> '{NewValue}'", ViewModel.DinnerTemplate.IsEatenOut,
                editor.IsEatenOut);

            logger.LogDebug("IsReadyMadeDish: '{OldValue}' -> '{NewValue}'", ViewModel.DinnerTemplate.IsReadyMadeDish,
                editor.IsReadyMadeDish);

            ViewModel.DinnerTemplate.Name = editor.Name;
            ViewModel.DinnerTemplate.IsTakeAway = editor.IsTakeAway;
            ViewModel.DinnerTemplate.HasLeftovers = editor.HasLeftovers;
            ViewModel.DinnerTemplate.LeftoversEnoughForDinner = editor.LeftoversEnoughForDinner;
            ViewModel.DinnerTemplate.IsLeftovers = editor.IsLeftovers;
            ViewModel.DinnerTemplate.IsEatenOut = editor.IsEatenOut;
            ViewModel.DinnerTemplate.IsReadyMadeDish = editor.IsReadyMadeDish;
        }

        private void ApplyDinnerTemplateToEditor()
        {
            ViewModel.DinnerTemplateEditor.Api.ApplyDinnerTemplate(ViewModel.DinnerTemplate);
        }

        public override async Task DeleteClicked(object sender, RoutedEventArgs e)
        {
            ContentDialogResult result = await ShowDeleteConfirmation("Delete dinner template?",
                "This will permanently delete the current dinner template.");

            if (result != ContentDialogResult.Primary)
            {
                return;
            }

            await queryService.DeleteEntityById(ViewModel.DinnerTemplate.Id);

            logger.LogInformation("Deleted dinner template {DinnerTemplateId}", ViewModel.DinnerTemplate.Id);

            NavigateBack();
        }

        protected override void SetEditorReadOnly(bool isReadOnly)
        {
            ViewModel.DinnerTemplateEditor.Api.SetReadOnly(isReadOnly);
        }

        protected override async Task SaveChanges()
        {
            ApplyEditorValuesToDinnerTemplate();

            await queryService.UpdateEntity(ViewModel.DinnerTemplate);

            logger.LogInformation("Saved changes to dinner template {DinnerTemplateId}.", ViewModel.DinnerTemplate.Id);
        }

        protected override void ApplyEntityToEditor()
        {
            ApplyDinnerTemplateToEditor();
        }

        protected override void UpdateHasChanges()
        {
            DinnerTemplateEditor.IDinnerTemplateEditorApi editor = ViewModel.DinnerTemplateEditor.Api;

            DinnerTemplate dinnerTemplate = ViewModel.DinnerTemplate;

            ViewModel.HasChanges = editor.Name != dinnerTemplate.Name ||
                                   editor.IsTakeAway != dinnerTemplate.IsTakeAway ||
                                   editor.HasLeftovers != dinnerTemplate.HasLeftovers ||
                                   editor.LeftoversEnoughForDinner != dinnerTemplate.LeftoversEnoughForDinner ||
                                   editor.IsLeftovers != dinnerTemplate.IsLeftovers ||
                                   editor.IsEatenOut != dinnerTemplate.IsEatenOut ||
                                   editor.IsReadyMadeDish != dinnerTemplate.IsReadyMadeDish;

            UpdateSaveAndCancelText();
        }

        protected override void NavigateBack()
        {
            ViewModel.Arguments.NavigationService.NavigateBack();
        }

        protected override void LogSaveError(Exception exception)
        {
            logger.LogError(exception, "Failed to save changes to the dinner template.");
        }
    }
}
