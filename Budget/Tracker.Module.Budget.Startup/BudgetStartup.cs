//using Tracker.Module.Budget.Persistence.Query;

using Microsoft.Extensions.DependencyInjection;
using Tracker.Module.Budget.Model.Entity;
using Tracker.Module.Budget.Model.Searchable;
using Tracker.Module.Budget.Persistence.Query;
using Tracker.Shared.Abstraction.Interfaces.Startup;
using Tracker.Shared.Startup;
using Tracker.Shared.Startup.Modules;

namespace Tracker.Module.Budget.Startup;

public class BudgetStartup<TApplicationBuilder> : ModularStartup<TApplicationBuilder>
{
    public BudgetStartup() : base()
    {
        AddModule(new EntityQueryServiceStartupModule<PaymentQueryService, Payment, SearchablePayment>());
        AddModule(
            new EntityQueryServiceStartupModule<PaymentTemplateQueryService, PaymentTemplate,
                SearchablePaymentTemplate>());
        AddModule(new EntityQueryServiceStartupModule<PaymentTypeQueryService, PaymentType, SearchablePaymentType>());
        AddModule(
            new EntityQueryServiceStartupModule<RecurringPaymentQueryService, RecurringPayment,
                SearchableRecurringPayment>());
        AddModule(
            new EntityQueryServiceStartupModule<CurrencyRateQueryService, CurrencyRate, SearchableCurrencyRate>());
    }

    /// <inheritdoc />
    protected override void ConfigureModuleServices(IServiceCollection services)
    {
        base.ConfigureModuleServices(services);

        // Register any pages that should be possible to navigate to from the left side menu.
        //services.AddSingleton<IPageRegion, SomePageRegionDefinition>();
    }

    /// <inheritdoc />
    protected override string ModuleName => "Budget Startup Module";
}
