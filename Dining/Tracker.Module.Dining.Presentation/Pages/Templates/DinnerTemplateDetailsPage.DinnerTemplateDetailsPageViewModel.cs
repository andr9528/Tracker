using CommunityToolkit.Mvvm.ComponentModel;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Module.Dining.Presentation.Pieces.Dinners;
using Tracker.Shared.Frontend.Core.Details;

namespace Tracker.Module.Dining.Presentation.Pages.Templates;

internal sealed partial class DinnerTemplateDetailsPage
{
    internal sealed partial class DinnerTemplateDetailsPageViewModel(DinnerTemplateDetailsPageArguments arguments)
        : BaseDetailsPageViewModel
    {
        public DinnerTemplateDetailsPageArguments Arguments { get; } = arguments;

        [ObservableProperty] private DinnerTemplate dinnerTemplate = null!;

        public DinnerTemplateEditor DinnerTemplateEditor { get; set; } = null!;
    }
}
