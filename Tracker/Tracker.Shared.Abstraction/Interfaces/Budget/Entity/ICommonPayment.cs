using Tracker.Shared.Abstraction.Interfaces.Budget.Searchable;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Entity
{
    public interface ICommonPayment : ISearchableCommonPayment
    {
        IPaymentType PaymentType { get; set; }
    }
}