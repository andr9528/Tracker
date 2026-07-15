using Tracker.Module.Budget.Model.ComplexSearchable;
using Tracker.Module.Budget.Model.Entity;
using Tracker.Module.Budget.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Budget.Persistence.Query;

public class
    PaymentTemplateQueryService : BaseEntityQueryService<TrackerDatabaseContext, PaymentTemplate,
    SearchablePaymentTemplate>
{
    public PaymentTemplateQueryService(TrackerDatabaseContext context) : base(context)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<PaymentTemplate> AddComplexQueryArguments(
        IQueryable<PaymentTemplate> query, IComplexSearchable<SearchablePaymentTemplate> complex)
    {
        if (complex is not ComplexSearchablePaymentTemplate)
        {
            return query;
        }

        return query;
    }

    /// <inheritdoc />
    protected override IEnumerable<PaymentTemplate> ApplyComplexNonDatabaseQueryArguments(
        IEnumerable<PaymentTemplate> entities, IComplexSearchable<SearchablePaymentTemplate> complex)
    {
        return entities;
    }

    /// <inheritdoc />
    protected override IQueryable<PaymentTemplate> GetBaseQuery()
    {
        return context.Set<PaymentTemplate>();
    }

    /// <inheritdoc />
    protected override IQueryable<PaymentTemplate> AddQueryArguments(
        SearchablePaymentTemplate searchable, IQueryable<PaymentTemplate> query)
    {
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

        if (searchable.RecurringPaymentId != default)
        {
            query = query.Where(x => x.RecurringPaymentId == searchable.RecurringPaymentId);
        }

        return query;
    }
}
