using Microsoft.EntityFrameworkCore;
using Tracker.Module.Budget.Persistence.Configuration;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Budget.Persistence;

public class BudgetDatabaseModule : IDatabaseModule
{
    /// <inheritdoc />
    public void Configure(ModelBuilder modelBuilder, DatabaseType databaseType)
    {
        modelBuilder.ApplyConfiguration(new CurrencyRateConfiguration(databaseType));
        modelBuilder.ApplyConfiguration(new PaymentConfiguration(databaseType));
        modelBuilder.ApplyConfiguration(new PaymentTemplateConfiguration(databaseType));
        modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration(databaseType));
        modelBuilder.ApplyConfiguration(new RecurringPaymentConfiguration(databaseType));
    }
}
