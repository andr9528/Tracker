using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Frontend;

public interface IEntitySelectionGridApi<T> where T : IEntity
{
    T? SelectedEntity { get; }

    event EventHandler? SelectionChanged;
}

