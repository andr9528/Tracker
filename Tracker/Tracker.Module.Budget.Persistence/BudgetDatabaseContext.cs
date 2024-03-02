using Microsoft.EntityFrameworkCore;
using Tracker.Module.Budget.Persistence.Configuration;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.User;
using Tracker.Shared.Persistence;
using Tracker.Shared.User.Persistence;

namespace Tracker.Module.Budget.Persistence
{
    public class BudgetDatabaseContext : IContextSegment
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<RecurringPayment> RecurringPayments { get; set; }
        public DbSet<PaymentTemplate> PaymentTemplates { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        /// <inheritdoc />
        public ContextSegmentType SegmentType => ContextSegmentType.BUDGET;

        /// <inheritdoc />
        public void OnModelCreating(ModelBuilder modelBuilder, DatabaseType databaseType)
        {
            modelBuilder.ApplyConfiguration(new PaymentConfiguration(databaseType));
            modelBuilder.ApplyConfiguration(new PaymentTemplateConfiguration(databaseType));
            modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration(databaseType));
            modelBuilder.ApplyConfiguration(new RecurringPaymentConfiguration(databaseType));
            modelBuilder.ApplyConfiguration(new CurrencyRateConfiguration(databaseType));
        }
    }
}