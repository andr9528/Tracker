using Microsoft.EntityFrameworkCore;
using Tracker.Module.Budget.Model.ComplexSearchable;
using Tracker.Module.Budget.Model.Entity;
using Tracker.Module.Budget.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Budget.Persistence.Query;

public class
    PaymentTypeQueryService : BaseEntityQueryService<TrackerDatabaseContext, PaymentType, SearchablePaymentType>
{
    public PaymentTypeQueryService(TrackerDatabaseContext context) : base(context)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<PaymentType> AddComplexQueryArguments(
        IQueryable<PaymentType> query, IComplexSearchable<SearchablePaymentType> complex)
    {
        if (complex is not ComplexSearchablePaymentType paymentTypeComplex)
        {
            return query;
        }

        if (!string.IsNullOrWhiteSpace(paymentTypeComplex.Name))
        {
            var keyword = $"%{paymentTypeComplex.Name}%";

            query = query.Where(x => EF.Functions.Like(x.Name, keyword));
        }

        if (!string.IsNullOrWhiteSpace(paymentTypeComplex.Description))
        {
            var keyword = $"%{paymentTypeComplex.Description}%";

            query = query.Where(x => EF.Functions.Like(x.Description, keyword));
        }

        return query;
    }

    /// <inheritdoc />
    protected override IEnumerable<PaymentType> ApplyComplexNonDatabaseQueryArguments(
        IEnumerable<PaymentType> entities, IComplexSearchable<SearchablePaymentType> complex)
    {
        return entities;
    }

    /// <inheritdoc />
    protected override IQueryable<PaymentType> GetBaseQuery()
    {
        return context.Set<PaymentType>();
    }

    /// <inheritdoc />
    protected override IQueryable<PaymentType> AddQueryArguments(
        SearchablePaymentType searchable, IQueryable<PaymentType> query)
    {
        if (!string.IsNullOrWhiteSpace(searchable.Name))
        {
            query = query.Where(x => x.Name.ToLower() == searchable.Name.ToLower());
        }

        if (!string.IsNullOrWhiteSpace(searchable.Description))
        {
            query = query.Where(x => x.Description.ToLower() == searchable.Description.ToLower());
        }

        return query;
    }
}
