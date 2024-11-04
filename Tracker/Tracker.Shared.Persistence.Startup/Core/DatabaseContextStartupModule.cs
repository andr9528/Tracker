using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tracker.Shared.Abstraction.Interfaces.Startup;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Shared.Persistence.Startup.Core
{
    public class DatabaseContextStartupModule<TContext> : IStartupModule where TContext : BaseDatabaseContext
    {
        public delegate void SetupOptionsDelegate(DbContextOptionsBuilder options);

        private readonly bool migrateOnStartup;
        private readonly SetupOptionsDelegate setupOptions;


        public DatabaseContextStartupModule(SetupOptionsDelegate setup, bool migrateOnStartup = true)
        {
            if (typeof(TContext) is {IsAbstract: true,})
                throw new ArgumentException($"Invalid type argument supplied to '{nameof(TContext)}'");

            this.migrateOnStartup = migrateOnStartup;
            setupOptions = setup ?? throw new ArgumentNullException(nameof(setup));
        }

        /// <inheritdoc />
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TContext>(options => setupOptions.Invoke(options));
        }

        /// <inheritdoc />
        public void ConfigureApplication(IApplicationBuilder app)
        {
            if (!migrateOnStartup)
                return;

            using IServiceScope service =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = service.ServiceProvider.GetService<TContext>();
            context?.Database.Migrate();
        }
    }
}