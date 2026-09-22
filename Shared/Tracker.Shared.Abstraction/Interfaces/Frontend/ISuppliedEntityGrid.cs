using Microsoft.UI.Xaml;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Frontend;

public interface ISuppliedEntityGrid<T> where T : IEntity
{
    void AddSuppliedEntity(T entity);

    void RemoveSuppliedEntity(T entity);

    T? SelectedEntity { get; }

    UIElement Content { get; }
}
