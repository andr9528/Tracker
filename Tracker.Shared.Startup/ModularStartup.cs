using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tracker.Shared.Abstraction.Interfaces.Startup;
using Uno.Extensions.Hosting;

namespace Tracker.Shared.Startup;

public abstract class ModularStartup : IStartupModule
{
    public IServiceCollection Services { get; private set; }
    public IServiceProvider ServiceProvider { get; private set; }

    private ICollection<IStartupModule> _modules;


    protected ModularStartup()
    {
        _modules = new List<IStartupModule>();
    }

    protected void AddModule(IStartupModule module)
    {
        _modules.Add(module);
    }

    /// <inheritdoc />
    public virtual void ConfigureServices(IServiceCollection services)
    {
    }

    /// <inheritdoc />
    public virtual void ConfigureApplication(IApplicationBuilder app)
    {
    }

    public void SetupServices(IServiceCollection? services = null)
    {
        Services = services ??= new ServiceCollection();

        ConfigureServices(services);
        foreach (IStartupModule module in _modules)
        {
            module.ConfigureServices(Services);
        }

        ServiceProvider = Services.BuildServiceProvider();
    }

    public IApplicationBuilder SetupApplication(IApplicationBuilder app)
    {
        ConfigureApplication(app);
        foreach (IStartupModule module in _modules)
        {
            module.ConfigureApplication(app);
        }

        return app;
    }
}
