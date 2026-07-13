using Tracker.Module.Budget.Model.ComplexSearchable;
using Tracker.Module.Budget.Model.Entity;
using Tracker.Module.Budget.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Budget.Persistence.Query;

public class PaymentQueryService : BaseEntityQueryService<TrackerDatabaseContext, Payment, SearchablePayment>
{
    public PaymentQueryService(TrackerDatabaseContext context) : base(context)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<Payment> AddComplexQueryArguments(
        IQueryable<Payment> query, IComplexSearchable<SearchablePayment> complex)
    {
        if (complex is not ComplexSearchablePayment)
        {
            return query;
        }

        return query;
    }

    /// <inheritdoc />
    protected override IEnumerable<Payment> ApplyComplexNonDatabaseQueryArguments(
        IEnumerable<Payment> entities, IComplexSearchable<SearchablePayment> complex)
    {
        return entities;
    }

    /// <inheritdoc />
    protected override IQueryable<Payment> GetBaseQuery()
    {
        return context.Set<Payment>();
    }

    /// <inheritdoc />
    protected override IQueryable<Payment> AddQueryArguments(SearchablePayment searchable, IQueryable<Payment> query)
    {
        if (searchable.Date != default)
        {
            query = query.Where(x => x.Date == searchable.Date);
        }

        if (searchable.RecurringPaymentId.HasValue)
        {
            query = query.Where(x => x.RecurringPaymentId == searchable.RecurringPaymentId);
        }

        if (searchable.Amount != default)
        {
            query = query.Where(x => x.Amount == searchable.Amount);
        }

        if (searchable.Currency != default)
        {
            query = query.Where(x => x.Currency == searchable.Currency);
        }

        if (searchable.PaymentTypeId != default)
        {
            query = query.Where(x => x.PaymentTypeId == searchable.PaymentTypeId);
        }

        return query;
    }
}
