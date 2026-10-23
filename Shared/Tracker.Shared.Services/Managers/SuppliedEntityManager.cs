using Tracker.Shared.Abstraction.Enums;

namespace Tracker.Shared.Services.Managers;

public sealed class SuppliedEntityManager<T>(GridEntitySource entitySource, Func<T, int> getId) where T : class
{
    private readonly List<T> suppliedEntities = [];

    public IReadOnlyCollection<T> SuppliedEntities => suppliedEntities.AsReadOnly();

    public List<T> ApplyEntitySource(IEnumerable<T> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        if (entitySource == GridEntitySource.ALL)
        {
            return entities.ToList();
        }

        var suppliedIds = suppliedEntities.Select(getId).ToHashSet();

        return entitySource switch
        {
            GridEntitySource.SUPPLIED => entities.Where(x => suppliedIds.Contains(getId(x))).ToList(),
            GridEntitySource.NOT_SUPPLIED => entities.Where(x => !suppliedIds.Contains(getId(x))).ToList(),
            var _ => entities.ToList(),
        };
    }

    public void Set(IEnumerable<T> entities)
    {
        EnsureSuppliedSource();
        ArgumentNullException.ThrowIfNull(entities);

        var uniqueEntities = entities.DistinctBy(getId).ToList();
        suppliedEntities.Clear();
        suppliedEntities.AddRange(uniqueEntities);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        EnsureSuppliedSource();
        ArgumentNullException.ThrowIfNull(entities);

        foreach (T entity in entities)
        {
            AddUnique(entity);
        }
    }

    public void Add(T entity)
    {
        EnsureSuppliedSource();
        ArgumentNullException.ThrowIfNull(entity);
        AddUnique(entity);
    }

    public void Remove(T entity)
    {
        EnsureSuppliedSource();
        ArgumentNullException.ThrowIfNull(entity);
        suppliedEntities.RemoveAll(x => getId(x) == getId(entity));
    }

    public void Clear()
    {
        EnsureSuppliedSource();
        suppliedEntities.Clear();
    }

    private void AddUnique(T entity)
    {
        if (suppliedEntities.All(x => getId(x) != getId(entity)))
        {
            suppliedEntities.Add(entity);
        }
    }

    private void EnsureSuppliedSource()
    {
        if (entitySource == GridEntitySource.ALL)
        {
            throw new InvalidOperationException(
                "Supplied entities can only be modified when the grid entity source is SUPPLIED or NOT_SUPPLIED.");
        }
    }
}
