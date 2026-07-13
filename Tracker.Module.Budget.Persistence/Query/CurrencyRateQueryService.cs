using Tracker.Module.Budget.Model.ComplexSearchable;
using Tracker.Module.Budget.Model.Entity;
using Tracker.Module.Budget.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Budget.Persistence.Query;

public class
    CurrencyRateQueryService : BaseEntityQueryService<TrackerDatabaseContext, CurrencyRate, SearchableCurrencyRate>
{
    public CurrencyRateQueryService(TrackerDatabaseContext context) : base(context)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<CurrencyRate> AddComplexQueryArguments(
        IQueryable<CurrencyRate> query, IComplexSearchable<SearchableCurrencyRate> complex)
    {
        if (complex is not ComplexSearchableCurrencyRate)
        {
            return query;
        }

        return query;
    }

    /// <inheritdoc />
    protected override IEnumerable<CurrencyRate> ApplyComplexNonDatabaseQueryArguments(
        IEnumerable<CurrencyRate> entities, IComplexSearchable<SearchableCurrencyRate> complex)
    {
        return entities;
    }

    /// <inheritdoc />
    protected override IQueryable<CurrencyRate> GetBaseQuery()
    {
        return context.Set<CurrencyRate>();
    }

    /// <inheritdoc />
    protected override IQueryable<CurrencyRate> AddQueryArguments(
        SearchableCurrencyRate searchable, IQueryable<CurrencyRate> query)
    {
        if (searchable.FromCurrency != default)
        {
            query = query.Where(x => x.FromCurrency == searchable.FromCurrency);
        }

        if (searchable.ToCurrency != default)
        {
            query = query.Where(x => x.ToCurrency == searchable.ToCurrency);
        }

        if (searchable.Rate != default)
        {
            query = query.Where(x => x.Rate == searchable.Rate);
        }

        if (searchable.Date != default)
        {
            query = query.Where(x => x.Date == searchable.Date);
        }

        return query;
    }
}
