using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tracker.Module.Budget.Persistence;
using Tracker.Module.Budget.Persistence.Query;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Models.User;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;
using Tracker.Shared.Startup;
using Tracker.Shared.User.Persistence;

namespace Tracker.Module.Budget.Api
{
    public class BudgetStartup : ModularStartup
    {
        public BudgetStartup(IConfiguration config) : base(config)
        {
            AddModule(
                new EntityQueryManagerStartupModule<PaymentQueryManager, Payment, SearchablePayment, PaymentDto>());
            AddModule(
                new EntityQueryManagerStartupModule<PaymentTemplateQueryManager, PaymentTemplate,
                    SearchablePaymentTemplate, PaymentTemplateDto>());
            AddModule(
                new EntityQueryManagerStartupModule<PaymentTypeQueryManager, PaymentType, SearchablePaymentType,
                    PaymentTypeDto>());
            AddModule(
                new EntityQueryManagerStartupModule<RecurringPaymentQueryManager, RecurringPayment,
                    SearchableRecurringPayment, RecurringPaymentDto>());
            AddModule(
                new EntityQueryManagerStartupModule<CurrencyRateQueryManager, CurrencyRate, SearchableCurrencyRate,
                    CurrencyRateDto>());
        }
    }
}