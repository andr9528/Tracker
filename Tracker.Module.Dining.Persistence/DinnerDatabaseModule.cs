using Microsoft.EntityFrameworkCore;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Dinner.Persistence;

public class DinnerDatabaseModule : IDatabaseModule
{
    public void Configure(ModelBuilder modelBuilder, DatabaseType databaseType)
    {
    }
}
