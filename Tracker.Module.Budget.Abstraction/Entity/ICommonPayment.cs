using Tracker.Module.Budget.Abstraction.Searchable;

namespace Tracker.Module.Budget.Abstraction.Entity;

public interface ICommonPayment : ISearchableCommonPayment
{
    IPaymentType PaymentType { get; set; }
}