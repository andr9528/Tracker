using Tracker.Shared.Abstraction.Interfaces.Frontend;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntitySelectionPiece<T> : Border where T : IEntity
{
    private EntitySelectionPieceViewModel ViewModel =>
        (EntitySelectionPieceViewModel) DataContext;

    private EntitySelectionPieceLogic Logic { get; }

    private EntitySelectionPieceUi Ui { get; }

    public IEntitySelectionPieceApi Api => Logic;

    public EntitySelectionPiece(EntitySelectionPieceArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePieceBorder();

        DataContext = new EntitySelectionPieceViewModel(arguments);

        Logic = new EntitySelectionPieceLogic(ViewModel);
        Ui = new EntitySelectionPieceUi(Logic, ViewModel);

        Child = Ui.CreateContentGrid();
    }

    public sealed record EntitySelectionPieceArguments(
        string Header,
        ISuppliedEntityGrid<T> AvailableGrid,
        ISuppliedEntityGrid<T> SuppliedGrid);

    public interface IEntitySelectionPieceApi
    {
        void SetReadOnly(bool isReadOnly);
    }
}
