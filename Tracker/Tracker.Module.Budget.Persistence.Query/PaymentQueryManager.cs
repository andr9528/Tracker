﻿using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.Query
{
    public class PaymentQueryManager : BaseBudgetQueryManager<Payment, SearchablePayment, PaymentDto>
    {
        /// <inheritdoc />
        public PaymentQueryManager(TrackerDatabaseContext context) : base(context)
        {
        }

        /// <inheritdoc />
        protected override Payment BuildEntity(PaymentDto dto)
        {
            return Payment.BuildPayment(dto);
        }

        /// <inheritdoc />
        protected override IQueryable<Payment> GetBaseQuery()
        {
            return GetContextSegment().Payments.AsQueryable();
        }

        /// <inheritdoc />
        protected override IQueryable<Payment> AddQueryArguments(
            SearchablePayment searchable, IQueryable<Payment> query)
        {
            return query;
        }
    }
}