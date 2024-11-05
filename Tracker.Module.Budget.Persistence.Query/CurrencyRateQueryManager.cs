using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;
using Tracker.Shared.Persistence.Segments;

namespace Tracker.Module.Budget.Persistence.Query
{
    public class CurrencyRateQueryManager : BaseEntityQueryManager<CurrencyRate, SearchableCurrencyRate,
        IBudgetContextSegment>
    {
        /// <inheritdoc />
        public CurrencyRateQueryManager(BaseDatabaseContext context, IBudgetContextSegment budgetContextSegment) : base(
            context, budgetContextSegment)
        {
        }

        /// <inheritdoc />
        protected override IQueryable<CurrencyRate> GetBaseQuery()
        {
            return segment.CurrencyRates.AsQueryable();
        }

        /// <inheritdoc />
        protected override IQueryable<CurrencyRate> AddQueryArguments(
            SearchableCurrencyRate searchable, IQueryable<CurrencyRate> query)
        {
            return query;
        }
    }
}