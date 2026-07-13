using Tracker.Module.Budget.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Budget.Model.ComplexSearchable;

public class ComplexSearchablePaymentTemplate : IComplexSearchable<SearchablePaymentTemplate>
{
    /// <inheritdoc />
    public SearchablePaymentTemplate Searchable { get; set; } = new();
}
