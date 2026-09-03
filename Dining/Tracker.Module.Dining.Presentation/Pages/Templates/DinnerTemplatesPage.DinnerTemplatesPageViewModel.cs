using CommunityToolkit.Mvvm.ComponentModel;
using Tracker.Module.Dining.Presentation.Pieces.Dinners;

namespace Tracker.Module.Dining.Presentation.Pages.Templates;

internal sealed partial class DinnerTemplatesPage
{
    internal sealed partial class DinnerTemplatesPageViewModel(DinnerTemplatesPageArguments arguments)
        : ObservableObject
    {
        public DinnerTemplatesPageArguments Arguments { get; } = arguments;

        internal DinnerTemplatesGrid DinnerTemplatesGrid { get; set; } = null!;
    }
}
