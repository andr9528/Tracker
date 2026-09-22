using Tracker.Shared.Abstraction.Interfaces.Frontend;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Pieces;

namespace Tracker.Shared.Frontend.Core;

public abstract class BaseArgumentsFactory
{
    public NullableBooleanOptionBar.NullableBooleanOptionBarArguments CreateNullableBooleanOptionBarArguments(
        string header, bool? initialValue = null)
    {
        return new NullableBooleanOptionBar.NullableBooleanOptionBarArguments(header, initialValue);
    }

    public EntitySelectionPiece<T>.EntitySelectionPieceArguments CreateEntitySelectionPieceArguments<T>(
        string header, ISuppliedEntityGrid<T> availableGrid, ISuppliedEntityGrid<T> suppliedGrid) where T : IEntity
    {
        return new EntitySelectionPiece<T>.EntitySelectionPieceArguments(header, availableGrid, suppliedGrid);
    }
}
