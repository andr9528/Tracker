using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Abstraction.Searchable;

public interface ISearchableDishIngredient : ISearchable
{
    int DishId { get; set; }
    int IngredientId { get; set; }
}
