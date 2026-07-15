using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tracker.Shared.Abstraction.Interfaces.Startup;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Shared.Startup.Modules;

public class DatabaseContextStartupModule<TContext> : IServiceStartupModule
    where TContext : BaseDatabaseContext<TContext>
{
    public delegate void SetupOptionsDelegate(DbContextOptionsBuilder options);

    protected readonly bool migrateOnStartup;
    private readonly SetupOptionsDelegate setupOptions;

    public DatabaseContextStartupModule(SetupOptionsDelegate setup, bool migrateOnStartup = true)
    {
        if (typeof(TContext) is {IsAbstract: true,})
        {
            throw new ArgumentException($"Invalid type argument supplied to '{nameof(TContext)}'");
        }

        this.migrateOnStartup = migrateOnStartup;
        setupOptions = setup ?? throw new ArgumentNullException(nameof(setup));
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<TContext>(options => setupOptions.Invoke(options));

        ServiceProvider? provider = services.BuildServiceProvider();
        using IServiceScope scope = provider.CreateScope();
        var logger = scope.ServiceProvider.GetService<ILogger<DatabaseContextStartupModule<TContext>>>();
        logger?.LogDebug("Completed Configuration of Database Services.");

        if (!migrateOnStartup)
        {
            return;
        }

        using var context = scope.ServiceProvider.GetService<TContext>();
        context?.Database.Migrate();

        logger?.LogDebug("Completed Migration of Database.");
    }

    /// <inheritdoc />
    public string Name => "Database Module";
}
