namespace Tracker.Shared.Abstraction.Interfaces.Persistence;

// Todo: Consider at better name...

/// <summary>
/// Add Implementation for things that need a Complex search.
/// </summary>
/// <typeparam name="TSearchable"></typeparam>
public interface IComplexSearchable<TSearchable> where TSearchable : class, ISearchable, new()
{
    TSearchable Searchable { get; set; }
}
