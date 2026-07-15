using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Abstraction.Searchable;

public interface ISearchableDish : ISearchable
{
    string Name { get; set; }
}
