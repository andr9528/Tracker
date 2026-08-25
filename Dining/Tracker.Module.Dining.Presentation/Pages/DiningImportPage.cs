using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Abstraction.Services;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Frontend.Abstraction;

namespace Tracker.Module.Dining.Presentation.Pages;

internal sealed partial class DiningImportPage : Page
{
    private DiningImportPageViewModel ViewModel =>
        (DiningImportPageViewModel) DataContext;

    public DiningImportPage(DiningImportPageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);

        DataContext = new DiningImportPageViewModel(arguments);

        var logic = new DiningImportPageLogic(ViewModel);
        var ui = new DiningImportPageUi(logic, ViewModel);

        Content = ui.CreateContentGrid();
    }


    internal sealed record DiningImportPageArguments(
        IDiningImportService ImportService,
        ILoggerFactory LoggerFactory,
        IMainWindowAccessor Accessor);
}
