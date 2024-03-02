using Microsoft.EntityFrameworkCore;
using Tracker.Module.Budget.Api;
using Tracker.Shared.Models.User;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;
using Tracker.Shared.Startup;
using Tracker.Shared.User.Persistence.Query;

namespace Tracker.Shared.Api
{
    public class MasterStartup : ModularStartup
    {
        private const string DATABASE_CONNECTION_STRING_NAME = "mainDb";

        public MasterStartup(IConfiguration config) : base(config)
        {
            AddModule(new ApiStartupModule());
            AddModule(new SwaggerStartupModule("Tracker"));

            AddModule(new DatabaseContextStartupModule<TrackerDatabaseContext>((options) =>
                options.UseSqlite(Configuration.GetConnectionString(DATABASE_CONNECTION_STRING_NAME))));

            AddModule(
                new EntityQueryManagerStartupModule<CoreUserQueryManager, CoreUser, SearchableCoreUser, CoreUserDto>());

            AddModule(new BudgetStartup(config));
        }
    }
}