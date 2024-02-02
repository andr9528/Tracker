using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.QueryManagers
{
    public class
        PaymentTypeQueryManager : BaseEntityQueryManager<BudgetDatabaseContext, PaymentType, SearchablePaymentType>
    {
        /// <inheritdoc />
        public PaymentTypeQueryManager(BudgetDatabaseContext context) : base(context)
        {
        }
    }
}