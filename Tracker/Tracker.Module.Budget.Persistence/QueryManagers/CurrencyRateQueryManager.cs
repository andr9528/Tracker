using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.QueryManagers
{
    public class
        CurrencyRateQueryManager : BaseEntityQueryManager<BudgetDatabaseContext, CurrencyRate, SearchableCurrencyRate>
    {
        /// <inheritdoc />
        public CurrencyRateQueryManager(BudgetDatabaseContext context) : base(context)
        {
        }
    }
}