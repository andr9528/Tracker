namespace Tracker.Shared.Abstraction.Interfaces.Startup;

public interface IApplicationStartupModule<TApplicationBuilder>
{
    string Name { get; }
    void ConfigureApplication(TApplicationBuilder app);
}
