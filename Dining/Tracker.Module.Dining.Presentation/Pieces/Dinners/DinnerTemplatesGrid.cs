using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Dinners;

internal sealed partial class DinnerTemplatesGrid : Border, INavigationRefreshable
{
    private DinnerTemplatesGridViewModel ViewModel =>
        (DinnerTemplatesGridViewModel) DataContext;

    private DinnerTemplatesGridLogic Logic { get; }

    public IDinnerTemplatesGridApi Api => Logic;

    public DinnerTemplatesGrid(DinnerTemplatesGridArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePieceBorder();

        DataContext = new DinnerTemplatesGridViewModel(arguments);

        Logic = new DinnerTemplatesGridLogic(ViewModel);
        var ui = new DinnerTemplatesGridUi(Logic, ViewModel);

        Child = ui.CreateContentGrid();

        _ = Logic.RefreshDinnerTemplates();
    }

    #region Implementation of INavigationRefreshable

    /// <inheritdoc />
    public void RefreshAfterNavigation()
    {
        var logger = ViewModel.Arguments.LoggerFactory.CreateLogger<DinnerTemplatesGrid>();
        logger.LogInformation("Refreshing Dinner Templates after Navigation");

        _ = Api.Refresh();
    }

    #endregion

    internal record DinnerTemplatesGridArguments(
        IEntityQueryService<DinnerTemplate, SearchableDinnerTemplate> QueryService,
        IUiDispatcher UiDispatcher,
        ILoggerFactory LoggerFactory,
        DiningArgumentsFactory ArgumentsFactory,
        GridDisplayMode DisplayMode = GridDisplayMode.NORMAL,
        int SelectedDinnerTemplateId = 0);

    internal interface IDinnerTemplatesGridApi
    {
        DinnerTemplate? SelectedDinnerTemplate { get; }

        Task Refresh();
    }
}
