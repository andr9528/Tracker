using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tracker.Shared.Abstraction.Interfaces.Startup;

namespace Tracker.Shared.Startup
{
    public abstract class ModularStartup : IStartupModule
    {
        public IConfiguration Configuration { get; private set; }
        public IServiceCollection Services { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }

        private ICollection<IStartupModule> _modules;


        protected ModularStartup(IConfiguration config)
        {
            Configuration = config ?? throw new ArgumentNullException(nameof(config));
            _modules = new List<IStartupModule>();
        }

        protected ModularStartup()
        {
            const string filename = "appsettings.json";
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), filename)))
                throw new FileNotFoundException(
                    $"Missing {filename} in main directory at {Path.Combine(Directory.GetCurrentDirectory(), filename)}");

            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(filename, false, true);

            Configuration = builder.Build();
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

            AddModule(this);
            foreach (IStartupModule module in _modules)
                module.ConfigureServices(Services);

            ServiceProvider = Services.BuildServiceProvider();
        }

        public void SetupApplication(IApplicationBuilder? app = null)
        {
            app ??= new ApplicationBuilder(ServiceProvider);

            foreach (IStartupModule module in _modules)
                module.ConfigureApplication(app);
        }
    }
}