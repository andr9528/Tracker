using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tracker.Shared.Abstraction.Interfaces.Startup;

namespace Tracker.Shared.Startup
{
    public class ApiStartupModule : IStartupModule
    {
        /// <inheritdoc />
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
        }

        /// <inheritdoc />
        public void ConfigureApplication(IApplicationBuilder app)
        {
            if (app.GetType().FullName != typeof(WebApplication).FullName)
            {
                throw new InvalidOperationException(
                    $"Expected application builder supplied to {nameof(ApiStartupModule)}.{nameof(ConfigureApplication)} to be of type {nameof(WebApplication)}, but it was of type '{app.GetType().FullName}'"
                    );
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            WebApplication castApp = (WebApplication) app;
            castApp.MapControllers();
        }
    }
}