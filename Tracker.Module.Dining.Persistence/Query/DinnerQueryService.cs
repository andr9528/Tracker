using Microsoft.EntityFrameworkCore;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Dining.Persistence.Query;

public class DinnerQueryService
    : BaseEntityQueryService<TrackerDatabaseContext, Dinner, SearchableDinner>
{
    public DinnerQueryService(TrackerDatabaseContext context) : base(context)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<Dinner> AddComplexQueryArguments(
        IQueryable<Dinner> query, IComplexSearchable<SearchableDinner> complex)
    {
        if (complex is not ComplexSearchableDinner searchable)
        {
            return query;
        }


        if (!string.IsNullOrWhiteSpace(searchable.Notes))
        {
            var keyword = $"%{searchable.Notes}%";

            query = query.Where(x => x.Notes != null && EF.Functions.Like(x.Notes, keyword));
        }

        if (searchable.IsTakeAway.HasValue)
        {
            query = query.Where(x => x.IsTakeAway == searchable.IsTakeAway.Value);
        }

        if (searchable.HasLeftovers.HasValue)
        {
            query = query.Where(x => x.HasLeftovers == searchable.HasLeftovers.Value);
        }

        if (searchable.LeftoversEnoughForDinner.HasValue)
        {
            query = query.Where(x =>
                x.LeftoversEnoughForDinner == searchable.LeftoversEnoughForDinner.Value);
        }

        if (searchable.IsLeftovers.HasValue)
        {
            query = query.Where(x => x.IsLeftovers == searchable.IsLeftovers.Value);
        }

        return query;
    }

    /// <inheritdoc />
    protected override IEnumerable<Dinner> ApplyComplexNonDatabaseQueryArguments(
        IEnumerable<Dinner> entities, IComplexSearchable<SearchableDinner> complex)
    {
        return entities;
    }

    /// <inheritdoc />
    protected override IQueryable<Dinner> GetBaseQuery()
    {
        return context.Set<Dinner>();
    }

    /// <inheritdoc />
    protected override IQueryable<Dinner> AddQueryArguments(
        SearchableDinner searchable, IQueryable<Dinner> query)
    {
        if (searchable.Date != default)
        {
            query = query.Where(x => x.Date == searchable.Date);
        }

        if (!string.IsNullOrWhiteSpace(searchable.Notes))
        {
            query = query.Where(x => x.Notes != null && x.Notes.ToLower() == searchable.Notes.ToLower());
        }

        if (searchable.DishId != default)
        {
            query = query.Where(x => x.DishId == searchable.DishId);
        }

        return query;
    }
}
