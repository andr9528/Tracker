using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.UI.Dispatching;
using Tracker.Frontend.Models;
using Tracker.Frontend.Services;
using Tracker.Module.Budget.Startup;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Persistence;
using Tracker.Shared.Services;
using Tracker.Shared.Services.Configuration;
using Tracker.Shared.Startup;
using Tracker.Shared.Startup.Configuration;
using Tracker.Shared.Startup.Modules;

namespace Tracker.Frontend;

public class UnoStartup : ModularStartup<IApplicationBuilder>
{
    public IHost? Host { get; private set; }

    private readonly IConfiguration configuration;
    private readonly ConfigurationService configurationService;

    public UnoStartup() : base()
    {
        configurationService = new ConfigurationService(GetType().Assembly);
        configuration = configurationService.BuildConfiguration();

        AddModule(new LoggingStartupModule(new[]
        {
            LogTarget.CONSOLE,
            LogTarget.FILE,
        }, configurationService.GetApplicationDataPath()));

        AddModule(new DatabaseContextStartupModule<TrackerDatabaseContext>(
            configurationService.ConfigureDatabaseOptions));

        AddModule(new BudgetStartup<IApplicationBuilder>());
    }

    /// <inheritdoc />
    protected override string ModuleName => "Uno Startup";

    /// <inheritdoc />
    protected override void ConfigureModuleServices(IServiceCollection services)
    {
        base.ConfigureModuleServices(services);

        services.AddSingleton(configurationService);
        services.AddSingleton<INavigationService, NavigationService>();

        foreach (IDatabaseModule module in TrackerDatabaseModules.Create())
        {
            services.AddSingleton<IDatabaseModule>(module);
        }

        DispatcherQueue uiDispatcherQueue = DispatcherQueue.GetForCurrentThread();
        services.AddSingleton(uiDispatcherQueue);
        services.AddSingleton<IUiDispatcher, UiDispatcher>();
    }

    /// <inheritdoc />
    protected override void ConfigureModuleApplication(IApplicationBuilder app)
    {
        base.ConfigureModuleApplication(app);

        app.Configure(host => host
#if DEBUG
            // Switch to Development environment when running in DEBUG
            .UseEnvironment(Environments.Development)
#endif
            .UseConfiguration(configure: ConfigureConfigurationSource).UseSerialization(ConfigureSerialization));

        Host = app.Build();
    }

    private IHostBuilder ConfigureConfigurationSource(IConfigBuilder configBuilder)
    {
        return configBuilder.EmbeddedSource<App>().Section<AppConfig>();
    }

    private void ConfigureSerialization(HostBuilderContext builderContext, IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton(new JsonSerializerOptions {IncludeFields = true,});
    }
}
