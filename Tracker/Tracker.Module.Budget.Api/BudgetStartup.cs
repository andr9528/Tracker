using Microsoft.EntityFrameworkCore;
using Tracker.Module.Budget.Persistence;
using Tracker.Module.Budget.Persistence.QueryManagers;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;
using Tracker.Shared.Models.User;
using Tracker.Shared.Persistence;
using Tracker.Shared.Startup;
using Tracker.Shared.User.Persistence;

namespace Tracker.Module.Budget.Api
{
    public class BudgetStartup : ModularStartup
    {
        private const string DATABASE_CONNECTION_STRING_NAME = "mainDb";

        public BudgetStartup(IConfiguration config) : base(config)
        {
            AddModule(new ApiStartupModule());
            AddModule(new SwaggerStartupModule("Budget"));

            AddModule(new DatabaseContextStartupModule<BudgetDatabaseContext>((options) =>
                options.UseSqlite(Configuration.GetConnectionString(DATABASE_CONNECTION_STRING_NAME))));
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
            AddModule(
                new EntityQueryManagerStartupModule<CoreUserQueryManager<BudgetDatabaseContext>, CoreUser,
                    SearchableCoreUser>());
        }
    }
}