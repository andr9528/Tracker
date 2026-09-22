using Tracker.Module.Dining.Model.Entity;

namespace Tracker.Module.Dining.Presentation.Pieces.Dishes;

internal sealed partial class DishesGrid
{
    internal sealed partial record DishGridItem(Dish Dish)
    {
        public int Id => Dish.Id;

        public string Name => Dish.Name;

        public int DinnerCount => Dish.Dinners.Count;

        public string Ingredients => string.Join(", ",
            Dish.DishIngredients.Select(x => x.Ingredient.Name).OrderBy(x => x));
    }
}
