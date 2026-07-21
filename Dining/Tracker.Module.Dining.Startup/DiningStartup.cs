using Microsoft.Extensions.DependencyInjection;
using Tracker.Module.Dining.Abstraction.Services;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Model.Searchable;
using Tracker.Module.Dining.Persistence.Query;
using Tracker.Module.Dining.Services;
using Tracker.Module.Dining.Services.Import;
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

        // Register any pages that should be possible to navigate to from the left side menu.
        //services.AddSingleton<IPageRegion, SomePageRegionDefinition>();

        services.AddSingleton<DiningSpreadsheetReader>();
        services.AddSingleton<IDiningImportService, DiningExcelImportService>();
    }

    /// <inheritdoc />
    protected override string ModuleName => "Dining Startup Module";
}
