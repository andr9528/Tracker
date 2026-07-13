using Tracker.Module.Budget.Abstraction.Searchable;

namespace Tracker.Module.Budget.Model.Searchable;

public class SearchablePaymentType : ISearchablePaymentType
{
    /// <inheritdoc />
    public int Id { get; set; }

    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public string Description { get; set; }
}
