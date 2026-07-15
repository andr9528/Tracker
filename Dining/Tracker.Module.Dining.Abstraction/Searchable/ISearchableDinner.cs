using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Abstraction.Searchable;

public interface ISearchableDinner : ISearchable
{
    DateOnly Date { get; set; }
    string? Notes { get; set; }


    int DishId { get; set; }
}
