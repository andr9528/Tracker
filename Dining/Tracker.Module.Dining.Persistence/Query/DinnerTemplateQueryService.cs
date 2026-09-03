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
        if (complex is not ComplexSearchableDinnerTemplate)
        {
            return query;
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
