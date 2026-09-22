using Tracker.Shared.Abstraction.Interfaces.Frontend;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntitySelectionPiece<T> : Border where T : IEntity
{
    private EntitySelectionPieceLogic Logic { get; }

    public EntitySelectionPiece(EntitySelectionPieceArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePieceBorder();

        Logic = new EntitySelectionPieceLogic(arguments);

        var ui = new EntitySelectionPieceUi(Logic, arguments);

        Child = ui.CreateContentGrid();
    }

    public sealed record EntitySelectionPieceArguments(
        IEntitySelectionGrid<T> AvailableGrid,
        ISuppliedEntityGrid<T> SuppliedGrid);
}
