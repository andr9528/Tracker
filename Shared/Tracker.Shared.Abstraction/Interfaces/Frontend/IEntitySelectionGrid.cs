using Microsoft.UI.Xaml;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Frontend;

public interface IEntitySelectionGrid<T> where T : IEntity
{
    IEntitySelectionGridApi<T> SelectionApi { get; }

    UIElement Content { get; }
}
