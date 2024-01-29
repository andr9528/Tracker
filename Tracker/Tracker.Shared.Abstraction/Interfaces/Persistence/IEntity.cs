namespace Tracker.Shared.Abstraction.Interfaces.Persistence
{
    public interface IEntity : ISearchable
    {

        byte[] Version { get; set; }
        DateTime CreatedDateTime { get; set; }
        DateTime UpdatedDateTime { get; set; }
    }
}