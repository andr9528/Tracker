using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Module.Budget.Abstraction.Searchable;

public interface ISearchableRecurringPayment : ISearchable
{
    DateOnly Start { get; set; }
    DateOnly? End { get; set; }
    int PaymentTemplateId { get; set; }
}
