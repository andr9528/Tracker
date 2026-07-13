using Tracker.Module.Budget.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Budget.Model.ComplexSearchable;

public class ComplexSearchablePayment : IComplexSearchable<SearchablePayment>
{
    /// <inheritdoc />
    public SearchablePayment Searchable { get; set; } = new();
}
