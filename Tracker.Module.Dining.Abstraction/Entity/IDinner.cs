using Tracker.Module.Dining.Abstraction.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Abstraction.Entity;

public interface IDinner : ISearchableDinner, IEntity
{
    IDish Dish { get; set; }

    bool IsTakeAway { get; set; }
    bool HasLeftovers { get; set; }
    bool LeftoversEnoughForDinner { get; set; }
    bool IsLeftovers { get; set; }
}
