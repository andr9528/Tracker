using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Budget.Abstraction.Searchable;

public interface ISearchablePaymentType : ISearchable
{
    string Name { get; set; }
    string Description { get; set; }
}
