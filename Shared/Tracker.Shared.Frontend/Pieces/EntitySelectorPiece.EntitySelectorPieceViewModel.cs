using CommunityToolkit.Mvvm.ComponentModel;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntitySelectorPiece<T>
{
    private sealed partial class EntitySelectorPieceViewModel(EntitySelectorPieceArguments arguments) : ObservableObject
    {
        [ObservableProperty] private string selectedEntityText = arguments.EmptySelectionText;
        internal EntitySelectorPieceArguments Arguments { get; } = arguments;
    }
}
