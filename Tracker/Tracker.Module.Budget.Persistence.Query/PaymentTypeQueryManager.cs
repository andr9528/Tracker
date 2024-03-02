using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.Query
{
    public class PaymentTypeQueryManager : BaseBudgetQueryManager<PaymentType, SearchablePaymentType, PaymentTypeDto>
    {
        /// <inheritdoc />
        public PaymentTypeQueryManager(TrackerDatabaseContext context) : base(context)
        {
        }

        /// <inheritdoc />
        protected override PaymentType BuildEntity(PaymentTypeDto dto)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override IQueryable<PaymentType> GetBaseQuery()
        {
            return GetContextSegment().PaymentTypes.AsQueryable();
        }

        /// <inheritdoc />
        protected override IQueryable<PaymentType> AddQueryArguments(
            SearchablePaymentType searchable, IQueryable<PaymentType> query)
        {
            return query;
        }
    }
}