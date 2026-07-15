using Tracker.Module.Dining.Abstraction.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Abstraction.Entity;

public interface IDishIngredient : ISearchableDishIngredient, IEntity
{
    IDish Dish { get; set; }
    IIngredient Ingredient { get; set; }
}
