using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Dinners;

internal sealed partial class DinnerTemplateEditor : Border
{
    public IDinnerTemplateEditorApi Api => Logic;

    internal DinnerTemplateEditorViewModel ViewModel =>
        (DinnerTemplateEditorViewModel) DataContext;

    private DinnerTemplateEditorLogic Logic { get; }

    private DinnerTemplateEditorUi Ui { get; }

    public DinnerTemplateEditor(DinnerTemplateEditorArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePieceBorder();

        DataContext = new DinnerTemplateEditorViewModel(arguments);

        Logic = new DinnerTemplateEditorLogic(ViewModel);
        Ui = new DinnerTemplateEditorUi(Logic, ViewModel);

        Child = Ui.CreateContentGrid();

        Logic.UpdateReadOnlyState();
    }

    internal sealed record DinnerTemplateEditorArguments(DinnerTemplate? DinnerTemplate = null, bool IsReadOnly = true);

    internal interface IDinnerTemplateEditorApi
    {
        string Name { get; }

        bool IsTakeAway { get; }
        bool HasLeftovers { get; }
        bool LeftoversEnoughForDinner { get; }
        bool IsLeftovers { get; }
        bool IsEatenOut { get; }
        bool IsReadyMadeDish { get; }

        void SetReadOnly(bool isReadOnly);
    }
}
