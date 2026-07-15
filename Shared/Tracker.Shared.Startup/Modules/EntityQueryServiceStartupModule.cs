using Microsoft.Extensions.DependencyInjection;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Abstraction.Interfaces.Startup;

namespace Tracker.Shared.Startup.Modules;

public class EntityQueryServiceStartupModule<TQuery, TEntity, TSearchable> : IServiceStartupModule
    where TQuery : class, IEntityQueryService<TEntity, TSearchable>
    where TEntity : class, IEntity
    where TSearchable : class, ISearchable, new()
{
    /// <inheritdoc />
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IEntityQueryService<TEntity, TSearchable>, TQuery>();
    }

    /// <inheritdoc />
    public string Name => $"Entity Query Service Module - {typeof(TQuery).Name}";
}
