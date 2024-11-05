using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Searchable
{
    public interface ISearchablePaymentType : ISearchable
    {
        string Name { get; set; }
        string Description { get; set; }
    }
}