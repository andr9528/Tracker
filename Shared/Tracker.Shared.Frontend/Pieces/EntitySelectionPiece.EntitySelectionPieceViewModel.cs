using CommunityToolkit.Mvvm.ComponentModel;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntitySelectionPiece<T>
{
    private sealed partial class EntitySelectionPieceViewModel(EntitySelectionPieceArguments arguments)
        : ObservableObject
    {
        [ObservableProperty] private bool isReadOnly;
        internal EntitySelectionPieceArguments Arguments { get; } = arguments;
    }
}
