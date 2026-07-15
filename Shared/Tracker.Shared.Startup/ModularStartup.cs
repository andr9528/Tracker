using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tracker.Shared.Abstraction.Interfaces.Startup;

namespace Tracker.Shared.Startup;

public abstract class ModularStartup<TApplicationBuilder> : IServiceStartupModule,
    IApplicationStartupModule<TApplicationBuilder>
{
    private readonly ICollection<IServiceStartupModule> serviceModules = [];
    private readonly ICollection<IApplicationStartupModule<TApplicationBuilder>> applicationModules = [];
    private readonly ICollection<ModularStartup<TApplicationBuilder>> modularStartupModules = [];
    private readonly Queue<Action<ILogger>> delayedLogActions = [];

    public IServiceCollection Services { get; private set; } = null!;

    public IServiceProvider ServiceProvider { get; private set; } = null!;

    public string Name => ModuleName;

    protected abstract string ModuleName { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        QueueStartupLog(logger => logger.LogDebug("Configuring services for {ModuleName}.", Name));

        ConfigureModuleServices(services);

        foreach (IServiceStartupModule module in serviceModules)
        {
            QueueStartupLog(logger =>
                logger.LogDebug("Configuring services for child module {ModuleName}.", module.Name));

            module.ConfigureServices(services);
        }
    }

    public void ConfigureApplication(TApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        QueueStartupLog(logger => logger.LogDebug("Configuring application for {ModuleName}.", Name));

        ConfigureModuleApplication(app);

        foreach (var module in applicationModules)
        {
            QueueStartupLog(logger =>
                logger.LogDebug("Configuring application for child module {ModuleName}.", module.Name));

            module.ConfigureApplication(app);
        }
    }

    protected virtual void ConfigureModuleServices(IServiceCollection services)
    {
    }

    protected virtual void ConfigureModuleApplication(TApplicationBuilder app)
    {
    }

    protected void AddModule<TModule>(TModule module) where TModule : class
    {
        ArgumentNullException.ThrowIfNull(module);

        var wasRegistered = false;

        if (module is ModularStartup<TApplicationBuilder> modularStartup)
        {
            AddModularStartupModule(modularStartup);
        }

        if (module is IServiceStartupModule serviceModule)
        {
            AddServiceModule(serviceModule);
            wasRegistered = true;
        }

        if (module is IApplicationStartupModule<TApplicationBuilder> applicationModule)
        {
            AddApplicationModule(applicationModule);
            wasRegistered = true;
        }

        if (!wasRegistered)
        {
            throw new ArgumentException(
                $"{typeof(TModule).Name} must implement " + $"{nameof(IServiceStartupModule)} and/or " +
                $"{nameof(IApplicationStartupModule<TApplicationBuilder>)}.", nameof(module));
        }
    }

    private void AddModularStartupModule(ModularStartup<TApplicationBuilder> module)
    {
        if (modularStartupModules.Contains(module))
        {
            throw new ArgumentException($"The modular startup module '{module.Name}' has already been registered.",
                nameof(module));
        }

        modularStartupModules.Add(module);
    }

    public void SetupServices(IServiceCollection? services = null)
    {
        Services = services ?? new ServiceCollection();

        ConfigureServices(Services);

        ServiceProvider = Services.BuildServiceProvider();
    }

    public TApplicationBuilder SetupApplication(TApplicationBuilder app)
    {
        ConfigureApplication(app);
        return app;
    }

    protected void QueueStartupLog(Action<ILogger> logAction)
    {
        ArgumentNullException.ThrowIfNull(logAction);
        delayedLogActions.Enqueue(logAction);
    }

    public void FlushStartupLogs()
    {
        ArgumentNullException.ThrowIfNull(ServiceProvider);

        var loggerFactory = ServiceProvider.GetRequiredService<ILoggerFactory>();

        FlushStartupLogs(loggerFactory);
    }

    private void FlushStartupLogs(ILoggerFactory loggerFactory)
    {
        ILogger logger = loggerFactory.CreateLogger(GetType());

        while (delayedLogActions.TryDequeue(out var logAction))
        {
            try
            {
                logAction(logger);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "A delayed startup log action failed.");
            }
        }

        foreach (var modularStartup in modularStartupModules)
        {
            modularStartup.FlushStartupLogs(loggerFactory);
        }
    }

    private void AddServiceModule(IServiceStartupModule module)
    {
        if (serviceModules.Contains(module))
        {
            throw new ArgumentException($"The service startup module '{module.Name}' " + "has already been registered.",
                nameof(module));
        }

        serviceModules.Add(module);

        QueueStartupLog(logger => logger.LogDebug("Registered {ModuleName} as a service startup module.", module.Name));
    }

    private void AddApplicationModule(IApplicationStartupModule<TApplicationBuilder> module)
    {
        if (applicationModules.Contains(module))
        {
            throw new ArgumentException(
                $"The application startup module '{module.Name}' " + "has already been registered.", nameof(module));
        }

        applicationModules.Add(module);

        QueueStartupLog(logger =>
            logger.LogDebug("Registered {ModuleName} as an application startup module.", module.Name));
    }
}
