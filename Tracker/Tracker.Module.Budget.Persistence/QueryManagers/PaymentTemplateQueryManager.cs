using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.QueryManagers
{
    public class PaymentTemplateQueryManager : BaseEntityQueryManager<BudgetDatabaseContext, PaymentTemplate,
        SearchablePaymentTemplate>
    {
        /// <inheritdoc />
        public PaymentTemplateQueryManager(BudgetDatabaseContext context) : base(context)
        {
        }
    }
}