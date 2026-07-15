using Microsoft.EntityFrameworkCore;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Time.Persistence;

public class TimeDatabaseModule : IDatabaseModule
{
    /// <inheritdoc />
    public void Configure(ModelBuilder modelBuilder, DatabaseType databaseType)
    {
    }
}
