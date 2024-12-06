using Uno.Extensions.Equality;
using Console = System.Console;

namespace Tracker.Frontend.Uno.Presentation;

public partial class ModulesNavigationViewModel : ObservableObject
{
    public ModulesNavigationViewModel()
    {
        Title = "Tracker";
    }

    public string? Title { get; }
}
