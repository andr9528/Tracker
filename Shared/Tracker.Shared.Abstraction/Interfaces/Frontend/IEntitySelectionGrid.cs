using Microsoft.UI.Xaml;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Frontend;

public interface IEntitySelectionGrid<T> where T : IEntity
{
    T? SelectedEntity { get; }
    UIElement Content { get; }
}
