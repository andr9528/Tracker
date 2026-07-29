using Tracker.Shared.Frontend.Factory;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class NullableBooleanOptionBar : Border
{
    public NullableBooleanOptionBarViewModel ViewModel =>
        (NullableBooleanOptionBarViewModel) DataContext;

    private NullableBooleanOptionBarUi Ui { get; }
    private NullableBooleanOptionBarLogic Logic { get; }

    public NullableBooleanOptionBar(NullableBooleanOptionBarArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePieceBorder();

        DataContext = new NullableBooleanOptionBarViewModel(arguments);

        Logic = new NullableBooleanOptionBarLogic(ViewModel);
        Ui = new NullableBooleanOptionBarUi(Logic, ViewModel);

        Child = Ui.CreateContentGrid();
    }

    public sealed record NullableBooleanOptionBarArguments(string Header, bool? InitialValue = null);
}
