using Microsoft.EntityFrameworkCore;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Models.User;
using Tracker.Shared.User.Persistence;
using Tracker.Module.Budget.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Shared.Persistence
{
    public class TrackerDatabaseContext : BaseDatabaseContext
    {
        public DbSet<CoreUser> CoreUsers { get; set; }

        /// <inheritdoc />
        public TrackerDatabaseContext(DbContextOptions options) : base(options)
        {
            AddContextSegment(new BudgetDatabaseContext());
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var databaseType = DatabaseType.SQlite;

            modelBuilder.ApplyConfiguration(new CoreUserConfiguration(databaseType));
            foreach (IContextSegment contextSegment in segments)
                contextSegment.OnModelCreating(modelBuilder, databaseType);
        }
    }
}