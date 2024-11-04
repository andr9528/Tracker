using Microsoft.EntityFrameworkCore;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Models.Modules.Budget.Entity;

namespace Tracker.Shared.Persistence.Segments
{
    public interface IBudgetContextSegment : IContextSegment
    {
        DbSet<Payment> Payments { get; set; }
        DbSet<RecurringPayment> RecurringPayments { get; set; }
        DbSet<PaymentTemplate> PaymentTemplates { get; set; }
        DbSet<PaymentType> PaymentTypes { get; set; }
        DbSet<CurrencyRate> CurrencyRates { get; set; }
    }
}