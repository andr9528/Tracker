using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;
using Tracker.Shared.Persistence.Core.Segments;

namespace Tracker.Module.Budget.Persistence.Query
{
    public class PaymentTemplateQueryManager : BaseEntityQueryManager<PaymentTemplate, SearchablePaymentTemplate,
        IBudgetContextSegment>
    {
        /// <inheritdoc />
        public PaymentTemplateQueryManager(BaseDatabaseContext context, IBudgetContextSegment budgetContextSegment) :
            base(context, budgetContextSegment)
        {
        }

        /// <inheritdoc />
        protected override IQueryable<PaymentTemplate> GetBaseQuery()
        {
            return segment.PaymentTemplates.AsQueryable();
        }

        /// <inheritdoc />
        protected override IQueryable<PaymentTemplate> AddQueryArguments(
            SearchablePaymentTemplate searchable, IQueryable<PaymentTemplate> query)
        {
            return query;
        }
    }
}