using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Frontend.Core;

namespace Tracker.Module.Dining.Presentation.Pages.Templates;

internal sealed partial class DinnerTemplatesPage
{
    internal sealed class DinnerTemplatesPageLogic(DinnerTemplatesPageViewModel viewModel)
        : BaseLogic<DinnerTemplatesPageViewModel>(viewModel)
    {
        public void CreateDinnerTemplateClicked(object sender, RoutedEventArgs e)
        {
            //DinnerTemplateCreationPage.DinnerTemplateCreationPageArguments arguments =
            //    ViewModel.Arguments.ArgumentsFactory.CreateDinnerTemplateCreationPageArguments();

            //var page = new DinnerTemplateCreationPage(arguments);

            //ViewModel.Arguments.NavigationService.NavigateTo(page, nameof(DinnerTemplateCreationPage));
        }

        public void ShowDetailsClicked(object sender, RoutedEventArgs e)
        {
            DinnerTemplate? selectedTemplate = ViewModel.DinnerTemplatesGrid.Api.SelectedDinnerTemplate;

            if (selectedTemplate is null)
            {
                return;
            }

            //DinnerTemplateDetailsPage.DinnerTemplateDetailsPageArguments arguments =
            //    ViewModel.Arguments.ArgumentsFactory.CreateDinnerTemplateDetailsPageArguments(selectedTemplate.Id);

            //var page = new DinnerTemplateDetailsPage(arguments);

            //ViewModel.Arguments.NavigationService.NavigateTo(page, nameof(DinnerTemplateDetailsPage));
        }
    }
}
