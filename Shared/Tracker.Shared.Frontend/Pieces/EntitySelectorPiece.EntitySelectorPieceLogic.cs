using Tracker.Shared.Frontend.Core;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntitySelectorPiece<T>
{
    private sealed class EntitySelectorPieceLogic(EntitySelectorPieceViewModel viewModel)
        : BaseLogic<EntitySelectorPieceViewModel>(viewModel), IEntitySelectorPieceApi
    {
        internal void Initialize()
        {
            ViewModel.Arguments.Grid.SelectionApi.SelectionChanged += GridSelectionChanged;
            UpdateSelectedEntityText();
        }

        private void GridSelectionChanged(object? sender, EventArgs e)
        {
            UpdateSelectedEntityText();
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateSelectedEntityText()
        {
            T? entity = SelectedEntity;

            ViewModel.SelectedEntityText = entity is null
                ? ViewModel.Arguments.EmptySelectionText
                : ViewModel.Arguments.SelectionTextFactory(entity);
        }

        #region Implementation of IEntitySelectorPieceApi

        /// <inheritdoc />
        public T? SelectedEntity => ViewModel.Arguments.Grid.SelectionApi.SelectedEntity;

        /// <inheritdoc />
        public event EventHandler? SelectionChanged;

        #endregion
    }
}
