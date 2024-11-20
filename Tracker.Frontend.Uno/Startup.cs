using Tracker.Module.Budget.Presentation;
using Tracker.Module.Budget.Presentation.ViewModel;
using Tracker.Module.Budget.Startup;
using Tracker.Shared.Startup;

namespace Tracker.Frontend.Uno;

public class Startup : ModularStartup
{
    public Startup() : base()
    {
        AddModule(new BudgetStartup());
    }

    /// <inheritdoc />
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        // Localization Hierarchy

        // Business Logic Services
    }

    /// <inheritdoc />
    public override void ConfigureApplication(IApplicationBuilder app)
    {
        base.ConfigureApplication(app);

        app.UseToolkitNavigation().Configure(host => host
#if DEBUG
            // Switch to Development environment when running in DEBUG
            .UseEnvironment(Environments.Development)
#endif
            .UseLogging(ConfigureLogging, true).UseSerilog(true, true)
            .UseConfiguration(configure: ConfigureConfigurationSource).UseLocalization(ConfigureLocalization)
            .UseNavigation(RegisterRoutes));
    }

    private void ConfigureLocalization(HostBuilderContext context, IServiceCollection services)
    {
        // Enables localization (see appsettings.json for supported languages)
    }

    private IHostBuilder ConfigureConfigurationSource(IConfigBuilder configBuilder)
    {
        return configBuilder.EmbeddedSource<App>().Section<AppConfig>();
    }


    private void ConfigureLogging(HostBuilderContext context, ILoggingBuilder logBuilder)
    {
        // Configure log levels for different categories of logging
        logBuilder.SetMinimumLevel(context.HostingEnvironment.IsDevelopment() ? LogLevel.Information : LogLevel.Warning)

            // Default filters for core Uno Platform namespaces
            .CoreLogLevel(LogLevel.Warning);

        // Uno Platform namespace filter groups
        // Uncomment individual methods to see more detailed logging
        //// Generic Xaml events
        //logBuilder.XamlLogLevel(LogLevel.Debug);
        //// Layout specific messages
        //logBuilder.XamlLayoutLogLevel(LogLevel.Debug);
        //// Storage messages
        //logBuilder.StorageLogLevel(LogLevel.Debug);
        //// Binding related messages
        //logBuilder.XamlBindingLogLevel(LogLevel.Debug);
        //// Binder memory references tracking
        //logBuilder.BinderMemoryReferenceLogLevel(LogLevel.Debug);
        //// DevServer and HotReload related
        //logBuilder.HotReloadCoreLogLevel(LogLevel.Information);
        //// Debug JS interop
        //logBuilder.WebAssemblyLogLevel(LogLevel.Debug);
    }

    private void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
    {
        RegisterViews(views);

        string budgetPath = TrackerModule.GetModuleAsReadableString(TrackerModule.Module.BUDGET);
        const string modulePath = "Module";

        var budgetRoutes = new RouteMap[]
        {
            new(BudgetTabs.BUDGET_TAB_REGION_NAME_ONE),
        };

        var routeLevelThree = new RouteMap[]
        {
            new(budgetPath, views.FindByViewModel<BudgetTabsViewModel>(), Nested: budgetRoutes),
        };
        var routeLevelTwo = new RouteMap[]
        {
            new(modulePath, views.FindByViewModel<ModulesNavigationViewModel>(), Nested: routeLevelThree),
        };
        var routeLevelOne = new RouteMap("", views.FindByViewModel<ShellViewModel>(), Nested: routeLevelTwo,
            IsDefault: true);

        routes.Register(routeLevelOne);
    }

    private void RegisterViews(IViewRegistry views)
    {
        var budgetViewMaps = new List<ViewMap>
        {
            new ViewMap<BudgetTabs, BudgetTabsViewModel>(),
            new ViewMap<PaymentsTab, PaymentsTab>(),
        };

        var viewMaps = new List<ViewMap>
        {
            new ViewMap<Shell, ShellViewModel>(),
            new ViewMap<ModulesNavigationPage, ModulesNavigationViewModel>(),
        };

        viewMaps.AddRange(budgetViewMaps);

        views.Register(viewMaps.ToArray());
    }
}
