using Tracker.Shared.Abstraction.Interfaces.User;

namespace Tracker.Shared.Models.User
{
    public class SearchableCoreUser : ISearchableCoreUser
    {
        /// <inheritdoc />
        public int Id { get; set; }
    }
}