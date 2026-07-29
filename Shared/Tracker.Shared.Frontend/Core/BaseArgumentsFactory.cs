using Tracker.Shared.Frontend.Pieces;

namespace Tracker.Shared.Frontend.Core;

public abstract class BaseArgumentsFactory
{
    public NullableBooleanOptionBar.NullableBooleanOptionBarArguments CreateNullableBooleanOptionBarArguments(
        string header, bool? initialValue = null)
    {
        return new NullableBooleanOptionBar.NullableBooleanOptionBarArguments(header, initialValue);
    }
}
