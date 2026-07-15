using Microsoft.EntityFrameworkCore;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Shared.Persistence
{
    public class TrackerDatabaseContext : BaseDatabaseContext<TrackerDatabaseContext>
    {
        private readonly IReadOnlyCollection<IDatabaseModule> databaseModules;

        /// <inheritdoc />
        public TrackerDatabaseContext(
            DbContextOptions<TrackerDatabaseContext> options,
            IEnumerable<IDatabaseModule> databaseModules) : base(options)
        {
            this.databaseModules = databaseModules.ToArray();
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var databaseType = DatabaseType.SQLITE;

            foreach (IDatabaseModule databaseModule in databaseModules)
            {
                databaseModule.Configure(modelBuilder, databaseType);
            }
        }
    }
}
