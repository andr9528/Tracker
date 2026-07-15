using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Tracker.Shared.Persistence;
using Tracker.Shared.Services.Configuration;

namespace Tracker.Shared.Startup.Configuration;

public sealed class TrackerDbDesignTimeContextFactory : IDesignTimeDbContextFactory<TrackerDatabaseContext>
{
    public TrackerDatabaseContext CreateDbContext(string[] args)
    {
        var configurationService = new ConfigurationService(GetType().Assembly);

        var optionsBuilder = new DbContextOptionsBuilder<TrackerDatabaseContext>();

        configurationService.ConfigureDatabaseOptions(optionsBuilder);

        return new TrackerDatabaseContext(optionsBuilder.Options, TrackerDatabaseModules.Create());
    }
}
