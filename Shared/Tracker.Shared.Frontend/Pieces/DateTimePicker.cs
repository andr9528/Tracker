using Microsoft.Extensions.Logging;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Shared.Frontend.Pieces;

internal sealed partial class DateTimePicker : Border
{
    internal DateTimePickerViewModel ViewModel => (DateTimePickerViewModel) DataContext;

    public DateTimePicker(DateTimePickerArguments arguments)
    {
        DataContext = new DateTimePickerViewModel(arguments);
        this.ConfigurePieceBorder();

        var logic = new DateTimePickerLogic(ViewModel);
        var ui = new DateTimePickerUi(logic, ViewModel);

        Child = ui.CreateContentGrid();
    }

    internal record DateTimePickerArguments(
        string Header,
        ILoggerFactory LoggerFactory,
        DateTime? InitialValue = null,
        int MinuteIncrement = 5);
}
