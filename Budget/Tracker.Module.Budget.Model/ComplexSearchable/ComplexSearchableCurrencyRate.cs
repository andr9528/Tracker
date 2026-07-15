using Tracker.Module.Budget.Model.Searchable;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Budget.Model.ComplexSearchable;

public class ComplexSearchableCurrencyRate : IComplexSearchable<SearchableCurrencyRate>
{
    /// <inheritdoc />
    public SearchableCurrencyRate Searchable { get; set; } = new();
}
