using Tracker.Shared.Frontend.Core;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntitySelectionPiece<T>
{
    private sealed class EntitySelectionPieceLogic(EntitySelectionPieceViewModel viewModel)
        : BaseLogic<EntitySelectionPieceViewModel>(viewModel), IEntitySelectionPieceApi
    {
        internal void AddClicked(object? sender, EventArgs e)
        {
            T? entity = ViewModel.Arguments.AvailableGrid.SelectedEntity;

            if (entity is null)
            {
                return;
            }

            ViewModel.Arguments.AvailableGrid.AddSuppliedEntity(entity);
            ViewModel.Arguments.SuppliedGrid.AddSuppliedEntity(entity);
        }

        internal void RemoveClicked(object? sender, EventArgs e)
        {
            T? entity = ViewModel.Arguments.SuppliedGrid.SelectedEntity;

            if (entity is null)
            {
                return;
            }

            ViewModel.Arguments.AvailableGrid.RemoveSuppliedEntity(entity);
            ViewModel.Arguments.SuppliedGrid.RemoveSuppliedEntity(entity);
        }

        #region Implementation of IEntitySelectionPieceApi

        /// <inheritdoc />
        public void SetReadOnly(bool isReadOnly)
        {
            ViewModel.IsReadOnly = isReadOnly;
        }

        #endregion
    }
}
