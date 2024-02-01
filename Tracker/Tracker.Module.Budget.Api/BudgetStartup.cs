using Microsoft.EntityFrameworkCore;
using Tracker.Module.Budget.Persistence;
using Tracker.Module.Budget.Persistence.QueryManagers;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Persistence;
using Tracker.Shared.Startup;

namespace Tracker.Module.Budget.Api
{
    public class BudgetStartup : ModularStartup
    {
        private const string DATABASE_CONNECTION_STRING_NAME = "mainDb";

        public BudgetStartup(IConfiguration config) : base(config)
        {
            AddModule(new ApiStartupModule());
            AddModule(new SwaggerStartupModule());

            AddModule(new DatabaseContextStartupModule<BudgetDatabaseContext>(
                (options) => options.UseSqlite(Configuration.GetConnectionString(DATABASE_CONNECTION_STRING_NAME)))
            );
            AddModule(new EntityQueryManagerStartupModule<PaymentQueryManager, Payment, SearchablePayment>());
        }


    }
}