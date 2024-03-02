using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Abstraction.Interfaces.Startup;

namespace Tracker.Shared.Persistence
{
    public class EntityQueryManagerStartupModule<TQuery, TEntity, TSearchable, TDto> : IStartupModule
        where TQuery : class, IEntityQueryManager<TEntity, TSearchable, TDto>
        where TEntity : class, IEntity
        where TSearchable : class, ISearchable
        where TDto : class, IDto
    {
        /// <inheritdoc />
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IEntityQueryManager<TEntity, TSearchable, TDto>, TQuery>();
        }

        /// <inheritdoc />
        public void ConfigureApplication(IApplicationBuilder app)
        {
        }
    }
}