using Tracker.Module.Budget.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Budget.Model.ComplexSearchable;

public class ComplexSearchablePaymentType : IComplexSearchable<SearchablePaymentType>
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    /// <inheritdoc />
    public SearchablePaymentType Searchable { get; set; } = new();
}
