namespace Tracker.Shared.Abstraction.Interfaces.Startup;

public interface IApplicationStartupModule<TApplicationBuilder>
{
    void ConfigureApplication(TApplicationBuilder app);
    string Name { get; }
}
