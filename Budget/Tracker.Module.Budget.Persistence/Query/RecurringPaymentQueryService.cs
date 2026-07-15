using Tracker.Module.Budget.Model.ComplexSearchable;
using Tracker.Module.Budget.Model.Entity;
using Tracker.Module.Budget.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Budget.Persistence.Query;

public class RecurringPaymentQueryService : BaseEntityQueryService<TrackerDatabaseContext, RecurringPayment,
    SearchableRecurringPayment>
{
    public RecurringPaymentQueryService(TrackerDatabaseContext context) : base(context)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<RecurringPayment> AddComplexQueryArguments(
        IQueryable<RecurringPayment> query, IComplexSearchable<SearchableRecurringPayment> complex)
    {
        if (complex is not ComplexSearchableRecurringPayment recurringPaymentComplex)
        {
            return query;
        }

        if (recurringPaymentComplex.StartFrom.HasValue)
        {
            query = query.Where(x => x.Start >= recurringPaymentComplex.StartFrom.Value);
        }

        if (recurringPaymentComplex.StartTo.HasValue)
        {
            query = query.Where(x => x.Start <= recurringPaymentComplex.StartTo.Value);
        }

        if (recurringPaymentComplex.EndFrom.HasValue)
        {
            query = query.Where(x => x.End.HasValue && x.End.Value >= recurringPaymentComplex.EndFrom.Value);
        }

        if (recurringPaymentComplex.EndTo.HasValue)
        {
            query = query.Where(x => x.End.HasValue && x.End.Value <= recurringPaymentComplex.EndTo.Value);
        }

        return query;
    }

    /// <inheritdoc />
    protected override IEnumerable<RecurringPayment> ApplyComplexNonDatabaseQueryArguments(
        IEnumerable<RecurringPayment> entities, IComplexSearchable<SearchableRecurringPayment> complex)
    {
        return entities;
    }

    /// <inheritdoc />
    protected override IQueryable<RecurringPayment> GetBaseQuery()
    {
        return context.Set<RecurringPayment>();
    }

    /// <inheritdoc />
    protected override IQueryable<RecurringPayment> AddQueryArguments(
        SearchableRecurringPayment searchable, IQueryable<RecurringPayment> query)
    {
        if (searchable.Start != default)
        {
            query = query.Where(x => x.Start == searchable.Start);
        }

        if (searchable.End.HasValue)
        {
            query = query.Where(x => x.End == searchable.End);
        }

        if (searchable.PaymentTemplateId != default)
        {
            query = query.Where(x => x.PaymentTemplateId == searchable.PaymentTemplateId);
        }

        return query;
    }
}
