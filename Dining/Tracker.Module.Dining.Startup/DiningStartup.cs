using Microsoft.Extensions.DependencyInjection;
using Tracker.Module.Dining.Abstraction.Services;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Persistence.Query;
using Tracker.Module.Dining.Presentation;
using Tracker.Module.Dining.Services;
using Tracker.Module.Dining.Services.Import;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Startup;
using Tracker.Shared.Startup.Modules;

namespace Tracker.Module.Dining.Startup;

public class DiningStartup<TApplicationBuilder> : ModularStartup<TApplicationBuilder>
{
    public DiningStartup() : base()
    {
        AddModule(new EntityQueryServiceStartupModule<DinnerQueryService, Dinner, SearchableDinner>());
        AddModule(
            new EntityQueryServiceStartupModule<DishIngredientQueryService, DishIngredient,
                SearchableDishIngredient>());
        AddModule(new EntityQueryServiceStartupModule<DishQueryService, Dish, SearchableDish>());
        AddModule(new EntityQueryServiceStartupModule<IngredientQueryService, Ingredient, SearchableIngredient>());
    }

    /// <inheritdoc />
    protected override void ConfigureModuleServices(IServiceCollection services)
    {
        base.ConfigureModuleServices(services);

        services.AddSingleton<DiningArgumentsFactory>();
        services.AddSingleton<DiningSpreadsheetReader>();

        services.AddSingleton<IDiningImportService, DiningExcelImportService>();
        services.AddSingleton<IPageRegion, DiningHomepageRegionDefinition>();
    }

    /// <inheritdoc />
    protected override string ModuleName => "Dining Startup Module";
}
