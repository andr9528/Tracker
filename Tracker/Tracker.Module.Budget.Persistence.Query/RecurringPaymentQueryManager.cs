﻿using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.Query
{
    public class RecurringPaymentQueryManager : BaseBudgetQueryManager<RecurringPayment, SearchableRecurringPayment,
        RecurringPaymentDto>
    {
        /// <inheritdoc />
        public RecurringPaymentQueryManager(TrackerDatabaseContext context) : base(context)
        {
        }

        /// <inheritdoc />
        protected override RecurringPayment BuildEntity(RecurringPaymentDto dto)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override IQueryable<RecurringPayment> GetBaseQuery()
        {
            return GetContextSegment().RecurringPayments.AsQueryable();
        }

        /// <inheritdoc />
        protected override IQueryable<RecurringPayment> AddQueryArguments(
            SearchableRecurringPayment searchable, IQueryable<RecurringPayment> query)
        {
            return query;
        }
    }
}