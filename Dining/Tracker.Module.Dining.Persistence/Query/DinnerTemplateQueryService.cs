using Microsoft.EntityFrameworkCore;
using Tracker.Module.Dining.Model.ComplexSearchable;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Dining.Persistence.Query;

public class
    DinnerTemplateQueryService : BaseEntityQueryService<TrackerDatabaseContext, DinnerTemplate,
    SearchableDinnerTemplate>
{
    public DinnerTemplateQueryService(TrackerDatabaseContext context) : base(context)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<DinnerTemplate> AddComplexQueryArguments(
        IQueryable<DinnerTemplate> query, IComplexSearchable<SearchableDinnerTemplate> complex)
    {
        if (complex is not ComplexSearchableDinnerTemplate searchable)
        {
            return query;
        }

        if (!string.IsNullOrWhiteSpace(searchable.Name))
        {
            var keyword = $"%{searchable.Name}%";

            query = query.Where(x => EF.Functions.Like(x.Name, keyword));
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
            query = query.Where(x => x.LeftoversEnoughForDinner == searchable.LeftoversEnoughForDinner.Value);
        }

        if (searchable.IsLeftovers.HasValue)
        {
            query = query.Where(x => x.IsLeftovers == searchable.IsLeftovers.Value);
        }

        if (searchable.IsEatenOut.HasValue)
        {
            query = query.Where(x => x.IsEatenOut == searchable.IsEatenOut.Value);
        }

        if (searchable.IsReadyMadeDish.HasValue)
        {
            query = query.Where(x => x.IsReadyMadeDish == searchable.IsReadyMadeDish.Value);
        }

        return query;
    }

    /// <inheritdoc />
    protected override IEnumerable<DinnerTemplate> ApplyComplexNonDatabaseQueryArguments(
        IEnumerable<DinnerTemplate> entities, IComplexSearchable<SearchableDinnerTemplate> complex)
    {
        return entities;
    }

    /// <inheritdoc />
    protected override IQueryable<DinnerTemplate> GetBaseQuery()
    {
        return context.Set<DinnerTemplate>();
    }

    /// <inheritdoc />
    protected override IQueryable<DinnerTemplate> AddQueryArguments(
        SearchableDinnerTemplate searchable, IQueryable<DinnerTemplate> query)
    {
        if (!string.IsNullOrWhiteSpace(searchable.Name))
        {
            query = query.Where(x => x.Name.ToLower() == searchable.Name.ToLower());
        }

        return query;
    }
}
