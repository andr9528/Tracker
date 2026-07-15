namespace Tracker.Shared.Frontend.Abstraction;

public interface INavigationService
{
    void RegisterContentFrame(Frame frame);
    void NavigateTo(UIElement element, string name);
    void NavigateBack();
}
