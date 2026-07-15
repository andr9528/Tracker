using Microsoft.EntityFrameworkCore;
using Tracker.Module.Dining.Persistence.Configuration;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dining.Persistence;

public class DinnerDatabaseModule : IDatabaseModule
{
    public void Configure(ModelBuilder modelBuilder, DatabaseType databaseType)
    {
        modelBuilder.ApplyConfiguration(new DinnerConfiguration(databaseType));
        modelBuilder.ApplyConfiguration(new DishConfiguration(databaseType));
        modelBuilder.ApplyConfiguration(new DishIngredientConfiguration(databaseType));
        modelBuilder.ApplyConfiguration(new IngredientConfiguration(databaseType));
    }
}
