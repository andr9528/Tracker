using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;
using Tracker.Shared.Persistence.Segments;

namespace Tracker.Module.Budget.Persistence.Query
{
    public class PaymentQueryManager : BaseEntityQueryManager<Payment, SearchablePayment, IBudgetContextSegment>
    {
        /// <inheritdoc />
        public PaymentQueryManager(BaseDatabaseContext context, IBudgetContextSegment budgetContextSegment) : base(
            context, budgetContextSegment)
        {
        }

        /// <inheritdoc />
        protected override IQueryable<Payment> GetBaseQuery()
        {
            return segment.Payments.AsQueryable();
        }

        /// <inheritdoc />
        protected override IQueryable<Payment> AddQueryArguments(
            SearchablePayment searchable, IQueryable<Payment> query)
        {
            return query;
        }
    }
}