namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntitySelectionPiece<T>
{
    private sealed class EntitySelectionPieceLogic(EntitySelectionPieceArguments arguments)
    {
        internal void AddClicked(object? sender, EventArgs e)
        {
            T? entity = arguments.AvailableGrid.SelectedEntity;

            if (entity is null)
            {
                return;
            }

            arguments.SuppliedGrid.AddSuppliedEntity(entity);
        }

        internal void RemoveClicked(object? sender, EventArgs e)
        {
            T? entity = arguments.SuppliedGrid.SelectedEntity;

            if (entity is null)
            {
                return;
            }

            arguments.SuppliedGrid.RemoveSuppliedEntity(entity);
        }
    }
}
