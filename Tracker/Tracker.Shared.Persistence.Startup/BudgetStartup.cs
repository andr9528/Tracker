using Microsoft.Extensions.Configuration;
using Tracker.Module.Budget.Persistence.Query;
using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence.Startup.Core;
using Tracker.Shared.Startup;

namespace Tracker.Shared.Persistence.Startup
{
    public class BudgetStartup : ModularStartup
    {
        public BudgetStartup(IConfiguration config) : base(config)
        {
            AddModule(new EntityQueryManagerStartupModule<PaymentQueryManager, Payment, SearchablePayment>());
            AddModule(
                new EntityQueryManagerStartupModule<PaymentTemplateQueryManager, PaymentTemplate,
                    SearchablePaymentTemplate>());
            AddModule(
                new EntityQueryManagerStartupModule<PaymentTypeQueryManager, PaymentType, SearchablePaymentType>());
            AddModule(
                new EntityQueryManagerStartupModule<RecurringPaymentQueryManager, RecurringPayment,
                    SearchableRecurringPayment>());
            AddModule(
                new EntityQueryManagerStartupModule<CurrencyRateQueryManager, CurrencyRate, SearchableCurrencyRate>());
        }
    }
}