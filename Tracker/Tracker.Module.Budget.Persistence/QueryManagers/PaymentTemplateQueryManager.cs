using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.QueryManagers
{
    public class PaymentTemplateQueryManager : BaseEntityQueryManager<BudgetDatabaseContext, PaymentTemplate,
        SearchablePaymentTemplate, PaymentTemplateDto>
    {
        /// <inheritdoc />
        public PaymentTemplateQueryManager(BudgetDatabaseContext context) : base(context)
        {
        }

        /// <inheritdoc />
        protected override PaymentTemplate BuildEntity(PaymentTemplateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}