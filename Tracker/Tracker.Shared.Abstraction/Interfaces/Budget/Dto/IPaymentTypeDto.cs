using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Abstraction.Interfaces.Budget.Dto
{
    public interface IPaymentTypeDto : IDto
    {
        string Name { get; set; }
        string Description { get; set; }
    }
}