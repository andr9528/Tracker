using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Dining.Persistence.Query;

public class DishIngredientQueryService
    : BaseEntityQueryService<TrackerDatabaseContext, DishIngredient, SearchableDishIngredient>
{
    public DishIngredientQueryService(TrackerDatabaseContext context) : base(context)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<DishIngredient> AddComplexQueryArguments(
        IQueryable<DishIngredient> query,
        IComplexSearchable<SearchableDishIngredient> complex)
    {
        if (complex is not ComplexSearchableDishIngredient)
        {
            return query;
        }

        return query;
    }

    /// <inheritdoc />
    protected override IEnumerable<DishIngredient> ApplyComplexNonDatabaseQueryArguments(
        IEnumerable<DishIngredient> entities,
        IComplexSearchable<SearchableDishIngredient> complex)
    {
        return entities;
    }

    /// <inheritdoc />
    protected override IQueryable<DishIngredient> GetBaseQuery()
    {
        return context.Set<DishIngredient>();
    }

    /// <inheritdoc />
    protected override IQueryable<DishIngredient> AddQueryArguments(
        SearchableDishIngredient searchable, IQueryable<DishIngredient> query)
    {
        if (searchable.DishId != default)
        {
            query = query.Where(x => x.DishId == searchable.DishId);
        }

        if (searchable.IngredientId != default)
        {
            query = query.Where(x => x.IngredientId == searchable.IngredientId);
        }

        return query;
    }
}
