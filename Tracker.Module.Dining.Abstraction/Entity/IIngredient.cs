using Tracker.Module.Dining.Abstraction.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Abstraction.Entity;

public interface IIngredient : ISearchableIngredient, IEntity
{
    ICollection<IDishIngredient> DishIngredients { get; set; }
}
