using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.QueryManagers
{
    public class CurrencyRateQueryManager : BaseEntityQueryManager<BudgetDatabaseContext, CurrencyRate,
        SearchableCurrencyRate, CurrencyRateDto>
    {
        /// <inheritdoc />
        public CurrencyRateQueryManager(BudgetDatabaseContext context) : base(context)
        {
        }

        /// <inheritdoc />
        protected override CurrencyRate BuildEntity(CurrencyRateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}