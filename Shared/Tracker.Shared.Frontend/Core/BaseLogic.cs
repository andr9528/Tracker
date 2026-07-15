namespace Tracker.Shared.Frontend.Core;

public abstract class BaseLogic<TViewModel> where TViewModel : class
{
    protected BaseLogic(TViewModel viewModel)
    {
        ViewModel = viewModel;
    }

    protected TViewModel ViewModel { get; }
}
