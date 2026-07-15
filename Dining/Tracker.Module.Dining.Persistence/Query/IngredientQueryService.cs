using Microsoft.EntityFrameworkCore;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Dining.Persistence.Query;

public class IngredientQueryService
    : BaseEntityQueryService<TrackerDatabaseContext, Ingredient, SearchableIngredient>
{
    public IngredientQueryService(TrackerDatabaseContext context) : base(context)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<Ingredient> AddComplexQueryArguments(
        IQueryable<Ingredient> query, IComplexSearchable<SearchableIngredient> complex)
    {
        if (complex is not ComplexSearchableIngredient searchable)
        {
            return query;
        }

        if (!string.IsNullOrWhiteSpace(searchable.Name))
        {
            var keyword = $"%{searchable.Name}%";

            query = query.Where(x => EF.Functions.Like(x.Name, keyword));
        }

        if (searchable.InStock.HasValue)
        {
            query = query.Where(x => x.InStock == searchable.InStock.Value);
        }

        return query;
    }

    /// <inheritdoc />
    protected override IEnumerable<Ingredient> ApplyComplexNonDatabaseQueryArguments(
        IEnumerable<Ingredient> entities, IComplexSearchable<SearchableIngredient> complex)
    {
        return entities;
    }

    /// <inheritdoc />
    protected override IQueryable<Ingredient> GetBaseQuery()
    {
        return context.Set<Ingredient>();
    }

    /// <inheritdoc />
    protected override IQueryable<Ingredient> AddQueryArguments(
        SearchableIngredient searchable, IQueryable<Ingredient> query)
    {
        if (!string.IsNullOrWhiteSpace(searchable.Name))
        {
            query = query.Where(x => x.Name.ToLower() == searchable.Name.ToLower());
        }

        return query;
    }
}
