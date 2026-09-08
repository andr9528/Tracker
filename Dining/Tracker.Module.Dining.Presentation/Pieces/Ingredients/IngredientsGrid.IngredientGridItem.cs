using Tracker.Module.Dining.Model.Entity;

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients;

internal sealed partial class IngredientsGrid
{
    internal sealed partial record IngredientGridItem(Ingredient Ingredient, float? AverageDaysBetweenUsage)
    {
        public int Id => Ingredient.Id;
        public string Name => Ingredient.Name;
        public bool InStock => Ingredient.InStock;
        public int DishCount => Ingredient.DishIngredients.Count;
    }
}
