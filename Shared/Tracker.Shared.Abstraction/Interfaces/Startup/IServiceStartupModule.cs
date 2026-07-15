using Microsoft.Extensions.DependencyInjection;

namespace Tracker.Shared.Abstraction.Interfaces.Startup;

public interface IServiceStartupModule
{
    void ConfigureServices(IServiceCollection services);
    string Name { get; }
}
