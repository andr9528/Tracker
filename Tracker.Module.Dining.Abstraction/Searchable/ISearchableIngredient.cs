using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Abstraction.Searchable;

public interface ISearchableIngredient : ISearchable
{
    string Name { get; set; }
    bool InStock { get; set; }
}
