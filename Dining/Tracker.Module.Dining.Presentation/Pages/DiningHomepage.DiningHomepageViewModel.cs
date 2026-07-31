using CommunityToolkit.Mvvm.ComponentModel;

namespace Tracker.Module.Dining.Presentation.Pages;

internal sealed partial class DiningHomepage
{
    internal sealed partial class DiningHomepageViewModel(DiningHomepageArguments arguments) : ObservableObject
    {
        public DiningHomepageArguments Arguments { get; } = arguments;

        [ObservableProperty] private string mostEatenDish = string.Empty;

        [ObservableProperty] private string leastEatenDish = string.Empty;
    }
}
