using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Abstraction.Interfaces.Startup;

namespace Tracker.Shared.Persistence
{
    public class EntityQueryManagerStartupModule<TQuery, TEntity, TSearchable> : IStartupModule
        where TQuery : class, IEntityQueryManager<TEntity, TSearchable>
        where TEntity : class, IEntity 
        where TSearchable : class, ISearchable
    {

        /// <inheritdoc />
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IEntityQueryManager<TEntity, TSearchable>, TQuery>();
        }

        /// <inheritdoc />
        public void ConfigureApplication(IApplicationBuilder app)
        {

        }
    }
}