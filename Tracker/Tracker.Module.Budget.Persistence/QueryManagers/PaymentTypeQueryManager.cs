using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.QueryManagers
{
    public class PaymentTypeQueryManager : BaseEntityQueryManager<BudgetDatabaseContext, PaymentType,
        SearchablePaymentType, PaymentTypeDto>
    {
        /// <inheritdoc />
        public PaymentTypeQueryManager(BudgetDatabaseContext context) : base(context)
        {
        }

        /// <inheritdoc />
        protected override PaymentType BuildEntity(PaymentTypeDto dto)
        {
            throw new NotImplementedException();
        }
    }
}