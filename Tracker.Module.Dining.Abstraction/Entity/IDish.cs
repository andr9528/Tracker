using Tracker.Module.Dining.Abstraction.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Abstraction.Entity;

public interface IDish : ISearchableDish, IEntity
{
    ICollection<IDinner> Dinners { get; set; }
    ICollection<IDishIngredient> DishIngredients { get; set; }
}
