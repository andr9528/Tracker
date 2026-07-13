using Tracker.Shared.Frontend.Abstraction;

namespace Tracker.Frontend.Presentation;

internal sealed partial class PageSelector
{
    private sealed class PageSelectorViewModel
    {
        public List<IPageRegion> Regions { get; set; } = [];
        public ListView MenuList { get; set; } = null!;
        public Frame ContentFrame { get; set; } = null!;
        public Frame PaneFrame { get; set; } = null!;
    }
}
