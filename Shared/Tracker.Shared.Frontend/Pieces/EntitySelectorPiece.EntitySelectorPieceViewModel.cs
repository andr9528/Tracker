using CommunityToolkit.Mvvm.ComponentModel;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntitySelectorPiece<T>
{
    private sealed partial class EntitySelectorPieceViewModel(EntitySelectorPieceArguments arguments) : ObservableObject
    {
        internal EntitySelectorPieceArguments Arguments { get; } = arguments;

        [ObservableProperty] private string selectedEntityText = arguments.EmptySelectionText;
    }
}
