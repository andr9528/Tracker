using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Abstraction.Attributes;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages.Templates;

/// <summary>
/// Creates a new dinner template. Editing is always enabled.
/// </summary>
[SkipOnBackNavigation]
internal sealed partial class DinnerTemplateCreationPage : Border
{
    public DinnerTemplateCreationPage(DinnerTemplateCreationPageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);

        DataContext = new DinnerTemplateCreationPageViewModel(arguments);
        this.ConfigurePageBorder();

        var viewModel = (DinnerTemplateCreationPageViewModel) DataContext;
        var logic = new DinnerTemplateCreationPageLogic(viewModel);
        var ui = new DinnerTemplateCreationPageUi(logic, viewModel);

        Child = ui.CreateContentGrid();
    }

    internal sealed record DinnerTemplateCreationPageArguments(
        IEntityQueryService<DinnerTemplate, SearchableDinnerTemplate> DinnerTemplateQueryService,
        INavigationService NavigationService,
        ILoggerFactory LoggerFactory,
        DiningArgumentsFactory ArgumentsFactory);
}
