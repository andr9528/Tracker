using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.QueryManagers
{
    public class RecurringPaymentQueryManager : BaseEntityQueryManager<BudgetDatabaseContext, RecurringPayment,
        SearchableRecurringPayment>
    {
        /// <inheritdoc />
        public RecurringPaymentQueryManager(BudgetDatabaseContext context) : base(context)
        {
        }
    }
}