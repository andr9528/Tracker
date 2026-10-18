using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Abstraction.Services;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pages;

internal sealed partial class DiningImportPage : Border
{
    public DiningImportPage(DiningImportPageArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePageBorder();

        DataContext = new DiningImportPageViewModel(arguments);

        var logic = new DiningImportPageLogic(ViewModel);
        var ui = new DiningImportPageUi(logic, ViewModel);

        Child = ui.CreateContentGrid();
    }

    private DiningImportPageViewModel ViewModel =>
        (DiningImportPageViewModel) DataContext;


    internal sealed record DiningImportPageArguments(
        IDiningImportService ImportService,
        ILoggerFactory LoggerFactory,
        IMainWindowAccessor Accessor);
}
