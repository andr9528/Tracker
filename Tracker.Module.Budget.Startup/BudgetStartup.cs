using Tracker.Module.Budget.Persistence.Query;
using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Persistence.Core.Startup;
using Tracker.Shared.Startup;

namespace Tracker.Module.Budget.Startup;

public class BudgetStartup : ModularStartup
{
    public BudgetStartup() : base()
    {
        AddModule(new EntityQueryManagerStartupModule<PaymentQueryManager, Payment, SearchablePayment>());
        AddModule(
            new EntityQueryManagerStartupModule<PaymentTemplateQueryManager, PaymentTemplate,
                SearchablePaymentTemplate>());
        AddModule(new EntityQueryManagerStartupModule<PaymentTypeQueryManager, PaymentType, SearchablePaymentType>());
        AddModule(
            new EntityQueryManagerStartupModule<RecurringPaymentQueryManager, RecurringPayment,
                SearchableRecurringPayment>());
        AddModule(
            new EntityQueryManagerStartupModule<CurrencyRateQueryManager, CurrencyRate, SearchableCurrencyRate>());
    }
}
