using Microsoft.Extensions.DependencyInjection;

namespace Tracker.Shared.Abstraction.Interfaces.Startup;

public interface IServiceStartupModule
{
    string Name { get; }
    void ConfigureServices(IServiceCollection services);
}
