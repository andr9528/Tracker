using Tracker.Module.Budget.Persistence;
using Tracker.Module.Dining.Persistence;
using Tracker.Module.Time.Persistence;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Startup.Configuration;

public static class TrackerDatabaseModules
{
    public static IReadOnlyCollection<IDatabaseModule> Create()
    {
        return
        [
            new DinnerDatabaseModule(),
            new TimeDatabaseModule(),
            new BudgetDatabaseModule(),
        ];
    }
}
