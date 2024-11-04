using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;
using Tracker.Shared.Persistence.Segments;

namespace Tracker.Module.Budget.Persistence.Query
{
    public class PaymentTypeQueryManager : BaseEntityQueryManager<PaymentType, SearchablePaymentType,
        IBudgetContextSegment>
    {
        /// <inheritdoc />
        public PaymentTypeQueryManager(TrackerDatabaseContext context, IBudgetContextSegment budgetContextSegment) :
            base(context, budgetContextSegment)
        {
        }

        /// <inheritdoc />
        protected override IQueryable<PaymentType> GetBaseQuery()
        {
            return segment.PaymentTypes.AsQueryable();
        }

        /// <inheritdoc />
        protected override IQueryable<PaymentType> AddQueryArguments(
            SearchablePaymentType searchable, IQueryable<PaymentType> query)
        {
            return query;
        }
    }
}