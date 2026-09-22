using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Frontend;

public interface ISuppliedEntityGrid<T> : IEntitySelectionGrid<T> where T : IEntity
{
    void AddSuppliedEntity(T entity);

    void RemoveSuppliedEntity(T entity);
}
