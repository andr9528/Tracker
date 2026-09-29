using Microsoft.EntityFrameworkCore;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Dining.Persistence.Query;

public class DishQueryService
    : BaseEntityQueryService<TrackerDatabaseContext, Dish, SearchableDish>
{
    public DishQueryService(TrackerDatabaseContext context) : base(context)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<Dish> AddComplexQueryArguments(
        IQueryable<Dish> query, IComplexSearchable<SearchableDish> complex)
    {
        if (complex is not ComplexSearchableDish searchable)
        {
            return query;
        }

        if (!string.IsNullOrWhiteSpace(searchable.Name))
        {
            var keyword = $"%{searchable.Name}%";

            query = query.Where(x => EF.Functions.Like(x.Name, keyword));
        }

        if (!string.IsNullOrWhiteSpace(searchable.Ingredients))
        {
            query = AddIngredientSearch(query, searchable.Ingredients);
        }

        return query;
    }

    private static IQueryable<Dish> AddIngredientSearch(IQueryable<Dish> query, string search)
    {
        string[] ingredients =
            search.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (string ingredient in ingredients)
        {
            var keyword = $"%{ingredient}%";

            query = query.Where(dish => dish.DishIngredients.Any(x => EF.Functions.Like(x.Ingredient.Name, keyword)));
        }

        return query;
    }

    /// <inheritdoc />
    protected override IEnumerable<Dish> ApplyComplexNonDatabaseQueryArguments(
        IEnumerable<Dish> entities, IComplexSearchable<SearchableDish> complex)
    {
        return entities;
    }

    /// <inheritdoc />
    protected override IQueryable<Dish> GetBaseQuery()
    {
        return context.Set<Dish>().Include(x => x.Dinners);
    }

    /// <inheritdoc />
    protected override IQueryable<Dish> AddQueryArguments(SearchableDish searchable, IQueryable<Dish> query)
    {
        if (!string.IsNullOrWhiteSpace(searchable.Name))
        {
            query = query.Where(x => x.Name.ToLower() == searchable.Name.ToLower());
        }

        return query;
    }
}
