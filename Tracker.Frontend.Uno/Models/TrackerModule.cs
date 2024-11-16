namespace Tracker.Frontend.Uno.Models;

public partial class TrackerModule : ObservableObject
{
    [ObservableProperty] private readonly string name;

    public TrackerModule(string name)
    {
        this.name = name;
    }
}
