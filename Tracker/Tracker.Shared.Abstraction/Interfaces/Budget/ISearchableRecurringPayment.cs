using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget
{
    public interface ISearchableRecurringPayment : ISearchable
    {
        DateOnly Start { get; set; }
        DateOnly? End { get; set; }
        int PaymentTemplateId { get; set; }
        int CoreUserId { get; set; }
    }
}