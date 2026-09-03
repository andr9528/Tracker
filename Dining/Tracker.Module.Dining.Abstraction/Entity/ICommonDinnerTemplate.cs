namespace Tracker.Module.Dining.Abstraction.Entity;

public interface ICommonDinnerTemplate
{
    bool IsTakeAway { get; set; }
    bool HasLeftovers { get; set; }
    bool LeftoversEnoughForDinner { get; set; }
    bool IsLeftovers { get; set; }
    bool IsEatenOut { get; set; }
    bool IsReadyMadeDish { get; set; }
}
