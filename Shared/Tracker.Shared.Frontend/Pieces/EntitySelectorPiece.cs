using Tracker.Shared.Abstraction.Interfaces.Frontend;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntitySelectorPiece<T> : Border where T : IEntity
{
    public EntitySelectorPiece(EntitySelectorPieceArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePieceBorder();

        DataContext = new EntitySelectorPieceViewModel(arguments);

        Logic = new EntitySelectorPieceLogic(ViewModel);
        Ui = new EntitySelectorPieceUi(Logic, ViewModel);

        Child = Ui.CreateContentGrid();

        Logic.Initialize();
    }

    private EntitySelectorPieceViewModel ViewModel =>
        (EntitySelectorPieceViewModel) DataContext;

    private EntitySelectorPieceLogic Logic { get; }

    private EntitySelectorPieceUi Ui { get; }

    public IEntitySelectorPieceApi Api => Logic;

    public sealed record EntitySelectorPieceArguments(
        string Header,
        IEntitySelectionGrid<T> Grid,
        Func<T, string> SelectionTextFactory,
        string EmptySelectionText = "None selected");

    public interface IEntitySelectorPieceApi
    {
        T? SelectedEntity { get; }

        event EventHandler? SelectionChanged;
    }
}
