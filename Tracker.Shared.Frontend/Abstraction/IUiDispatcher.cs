using Microsoft.UI.Dispatching;

namespace Tracker.Shared.Frontend.Abstraction;

public interface IUiDispatcher
{
    bool TryEnqueue(Action action);
}

public sealed class UiDispatcher(DispatcherQueue dispatcherQueue) : IUiDispatcher
{
    public bool TryEnqueue(Action action)
    {
        return dispatcherQueue.TryEnqueue(() => action());
    }
}
