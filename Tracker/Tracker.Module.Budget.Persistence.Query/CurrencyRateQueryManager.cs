using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.Query
{
    public class CurrencyRateQueryManager : BaseEntityQueryManager<TrackerDatabaseContext, CurrencyRate,
        SearchableCurrencyRate, CurrencyRateDto>
    {
        /// <inheritdoc />
        public CurrencyRateQueryManager(TrackerDatabaseContext context) : base(context)
        {
        }

        /// <inheritdoc />
        protected override CurrencyRate BuildEntity(CurrencyRateDto dto)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override IQueryable<CurrencyRate> GetBaseQuery()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override IQueryable<CurrencyRate> AddQueryArguments(
            SearchableCurrencyRate searchable, IQueryable<CurrencyRate> query)
        {
            throw new NotImplementedException();
        }
    }
}