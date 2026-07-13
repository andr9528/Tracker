using Tracker.Shared.Frontend.Factory;

namespace Tracker.Shared.Frontend.Pieces;

internal sealed partial class NullableBooleanOptionBar : Border
{
    internal NullableBooleanOptionBarViewModel ViewModel =>
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

    internal sealed record NullableBooleanOptionBarArguments(string Header);
}
