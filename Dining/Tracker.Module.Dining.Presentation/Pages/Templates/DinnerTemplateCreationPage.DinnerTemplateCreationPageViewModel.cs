using Tracker.Module.Dining.Presentation.Pieces.Dinners;

namespace Tracker.Module.Dining.Presentation.Pages.Templates;

internal sealed partial class DinnerTemplateCreationPage
{
    private sealed class DinnerTemplateCreationPageViewModel(DinnerTemplateCreationPageArguments arguments)
    {
        public DinnerTemplateCreationPageArguments Arguments { get; } = arguments;

        internal DinnerTemplateEditor DinnerTemplateEditor { get; set; } = null!;
    }
}
