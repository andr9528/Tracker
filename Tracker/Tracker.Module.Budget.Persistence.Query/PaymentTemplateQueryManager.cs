﻿using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.Query
{
    public class PaymentTemplateQueryManager : BaseEntityQueryManager<TrackerDatabaseContext, PaymentTemplate,
        SearchablePaymentTemplate, PaymentTemplateDto>
    {
        /// <inheritdoc />
        public PaymentTemplateQueryManager(TrackerDatabaseContext context) : base(context)
        {
        }

        /// <inheritdoc />
        protected override PaymentTemplate BuildEntity(PaymentTemplateDto dto)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override IQueryable<PaymentTemplate> GetBaseQuery()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override IQueryable<PaymentTemplate> AddQueryArguments(
            SearchablePaymentTemplate searchable, IQueryable<PaymentTemplate> query)
        {
            throw new NotImplementedException();
        }
    }
}