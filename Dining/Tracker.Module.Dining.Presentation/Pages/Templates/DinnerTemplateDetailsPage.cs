using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Templates;

internal sealed partial class DinnerTemplateDetailsPage : Border
{
    public DinnerTemplateDetailsPage(DinnerTemplateDetailsPageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePageBorder();

        DataContext = new DinnerTemplateDetailsPageViewModel(arguments);

        var viewModel = (DinnerTemplateDetailsPageViewModel) DataContext;
        var logic = new DinnerTemplateDetailsPageLogic(viewModel);
        var ui = new DinnerTemplateDetailsPageUi(logic, viewModel);

        Child = ui.CreateContentGrid();

        _ = logic.RefreshDinnerTemplate();
    }

    internal sealed record DinnerTemplateDetailsPageArguments(
        int DinnerTemplateId,
        IEntityQueryService<DinnerTemplate, SearchableDinnerTemplate> DinnerTemplateQueryService,
        IUiDispatcher UiDispatcher,
        ILoggerFactory LoggerFactory,
        INavigationService NavigationService,
        DiningArgumentsFactory ArgumentsFactory);
}
