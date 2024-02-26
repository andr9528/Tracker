using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.QueryManagers
{
    public class RecurringPaymentQueryManager : BaseEntityQueryManager<BudgetDatabaseContext, RecurringPayment,
        SearchableRecurringPayment, RecurringPaymentDto>
    {
        /// <inheritdoc />
        public RecurringPaymentQueryManager(BudgetDatabaseContext context) : base(context)
        {
        }

        /// <inheritdoc />
        protected override RecurringPayment BuildEntity(RecurringPaymentDto dto)
        {
            throw new NotImplementedException();
        }
    }
}