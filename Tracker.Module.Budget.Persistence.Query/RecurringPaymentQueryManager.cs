using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;
using Tracker.Shared.Persistence.Segments;

namespace Tracker.Module.Budget.Persistence.Query
{
    public class RecurringPaymentQueryManager : BaseEntityQueryManager<RecurringPayment, SearchableRecurringPayment,
        IBudgetContextSegment>
    {
        /// <inheritdoc />
        public RecurringPaymentQueryManager(BaseDatabaseContext context, IBudgetContextSegment budgetContextSegment) :
            base(context, budgetContextSegment)
        {
        }

        /// <inheritdoc />
        protected override IQueryable<RecurringPayment> GetBaseQuery()
        {
            return segment.RecurringPayments.AsQueryable();
        }

        /// <inheritdoc />
        protected override IQueryable<RecurringPayment> AddQueryArguments(
            SearchableRecurringPayment searchable, IQueryable<RecurringPayment> query)
        {
            return query;
        }
    }
}