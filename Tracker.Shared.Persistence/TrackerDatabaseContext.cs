using Microsoft.EntityFrameworkCore;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Models.User;
using Tracker.Shared.User.Persistence;
using Tracker.Module.Budget.Persistence;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence.Core;
using Tracker.Module.Budget.Persistence.Configuration;
using Tracker.Shared.Abstraction.Interfaces.Budget;
using Tracker.Shared.Abstraction.Interfaces.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Persistence.Core.Segments;

namespace Tracker.Shared.Persistence
{
    public class TrackerDatabaseContext : BaseDatabaseContext, IBudgetContextSegment, IBaselineContextSegment
    {
        /// <inheritdoc />
        public TrackerDatabaseContext(DbContextOptions options) : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var databaseType = DatabaseType.SQlite;

            modelBuilder.ApplyConfiguration(new CoreUserConfiguration(databaseType));

            modelBuilder.ApplyConfiguration(new PaymentConfiguration(databaseType));
            modelBuilder.ApplyConfiguration(new PaymentTemplateConfiguration(databaseType));
            modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration(databaseType));
            modelBuilder.ApplyConfiguration(new RecurringPaymentConfiguration(databaseType));
            modelBuilder.ApplyConfiguration(new CurrencyRateConfiguration(databaseType));
        }

        /// <inheritdoc />
        public DbSet<Payment> Payments { get; set; }

        /// <inheritdoc />
        public DbSet<RecurringPayment> RecurringPayments { get; set; }

        /// <inheritdoc />
        public DbSet<PaymentTemplate> PaymentTemplates { get; set; }

        /// <inheritdoc />
        public DbSet<PaymentType> PaymentTypes { get; set; }

        /// <inheritdoc />
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        /// <inheritdoc />
        public DbSet<CoreUser> CoreUsers { get; set; }
    }
}