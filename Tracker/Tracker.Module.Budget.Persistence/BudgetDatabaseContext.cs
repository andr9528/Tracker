using Microsoft.EntityFrameworkCore;
using Tracker.Module.Budget.Persistence.Configuration;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Persistence;

namespace Tracker.Module.Budget.Persistence
{
    public class BudgetDatabaseContext : BaseDatabaseContext
    {
        /// <inheritdoc />
        public BudgetDatabaseContext(DbContextOptions options) : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            const DatabaseType databaseType = DatabaseType.SQlite;

            modelBuilder.ApplyConfiguration(new PaymentConfiguration(databaseType));
        }
    }
}