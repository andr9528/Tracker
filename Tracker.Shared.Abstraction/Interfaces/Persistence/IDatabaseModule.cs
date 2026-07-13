using Microsoft.EntityFrameworkCore;
using Tracker.Shared.Abstraction.Enums.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Persistence;

public interface IDatabaseModule
{
    void Configure(ModelBuilder modelBuilder, DatabaseType databaseType);
}
