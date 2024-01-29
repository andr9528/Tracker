using System.Collections.ObjectModel;
using Tracker.Shared.Abstraction.Interfaces.Budget;
using Tracker.Shared.Abstraction.Interfaces.User;
using Tracker.Shared.Models.Modules.Budget;

namespace Tracker.Shared.Models.User
{
    public class CoreUser : ICoreUser
    {
        private readonly int id;

        /// <inheritdoc />
        public int Id
        {
            get => id;
            set => throw new InvalidOperationException($"{nameof(Id)} cannot be changed");
        }

        /// <inheritdoc />
        public byte[] Version { get; set; }

        /// <inheritdoc />
        public DateTime CreatedDateTime { get; set; }

        /// <inheritdoc />
        public DateTime UpdatedDateTime { get; set; }

        /// <inheritdoc />
        public ICollection<IPayment> Payments { get; set; }

        /// <inheritdoc />
        public ICollection<IRecurringPayment> RecurringPayments { get; set; }

        public CoreUser()
        {

        }

        private CoreUser(int id)
        {
            this.id = id;
        }
    }
}