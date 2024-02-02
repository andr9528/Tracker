﻿using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence.QueryManagers
{
    public class PaymentQueryManager : BaseEntityQueryManager<BudgetDatabaseContext, Payment, SearchablePayment>
    {
        /// <inheritdoc />
        public PaymentQueryManager(BudgetDatabaseContext context) : base(context)
        {
        }
    }
}