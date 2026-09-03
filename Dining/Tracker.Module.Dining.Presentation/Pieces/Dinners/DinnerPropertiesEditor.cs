using Tracker.Module.Dining.Abstraction.Entity;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Dinners;

internal sealed partial class DinnerPropertiesEditor : Border
{
    private DinnerPropertiesEditorViewModel ViewModel =>
        (DinnerPropertiesEditorViewModel) DataContext;

    private DinnerPropertiesEditorLogic Logic { get; }

    public IDinnerPropertiesEditorApi Api => Logic;

    public DinnerPropertiesEditor(DinnerPropertiesEditorArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePieceBorder();

        DataContext = new DinnerPropertiesEditorViewModel(arguments);

        Logic = new DinnerPropertiesEditorLogic(ViewModel);
        var ui = new DinnerPropertiesEditorUi(Logic, ViewModel);

        Child = ui.CreateContentGrid();

        Logic.UpdateReadOnlyState();
    }

    internal sealed record DinnerPropertiesEditorArguments(IDinnerTemplate? Template = null, bool IsReadOnly = true);

    internal interface IDinnerPropertiesEditorApi
    {
        bool IsTakeAway { get; }
        bool HasLeftovers { get; }
        bool LeftoversEnoughForDinner { get; }
        bool IsLeftovers { get; }
        bool IsEatenOut { get; }
        bool IsReadyMadeDish { get; }

        void ApplyTemplate(IDinnerTemplate template);
        void SetReadOnly(bool isReadOnly);
    }
}

