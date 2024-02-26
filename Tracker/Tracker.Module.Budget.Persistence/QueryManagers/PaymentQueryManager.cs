using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.QueryManagers
{
    public class PaymentQueryManager : BaseEntityQueryManager<BudgetDatabaseContext, Payment, SearchablePayment,
        PaymentDto>
    {
        /// <inheritdoc />
        public PaymentQueryManager(BudgetDatabaseContext context) : base(context)
        {
        }

        /// <inheritdoc />
        protected override Payment BuildEntity(PaymentDto dto)
        {
            throw new NotImplementedException();
        }
    }
}