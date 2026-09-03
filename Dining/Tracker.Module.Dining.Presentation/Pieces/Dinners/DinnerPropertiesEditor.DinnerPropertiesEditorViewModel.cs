using CommunityToolkit.Mvvm.ComponentModel;

namespace Tracker.Module.Dining.Presentation.Pieces.Dinners;

internal sealed partial class DinnerPropertiesEditor
{
    internal sealed partial class DinnerPropertiesEditorViewModel(DinnerPropertiesEditorArguments arguments)
        : ObservableObject
    {
        internal DinnerPropertiesEditorArguments Arguments { get; } = arguments;

        public event EventHandler? IsReadOnlyChanged;

        internal ICollection<CheckBox> CheckBoxes { get; } = [];

        [ObservableProperty] private bool isTakeAway = arguments.Template?.IsTakeAway ?? false;

        [ObservableProperty] private bool hasLeftovers = arguments.Template?.HasLeftovers ?? false;

        [ObservableProperty]
        private bool leftoversEnoughForDinner = arguments.Template?.LeftoversEnoughForDinner ?? false;

        [ObservableProperty] private bool isLeftovers = arguments.Template?.IsLeftovers ?? false;

        [ObservableProperty] private bool isEatenOut = arguments.Template?.IsEatenOut ?? false;

        [ObservableProperty] private bool isReadyMadeDish = arguments.Template?.IsReadyMadeDish ?? false;

        [ObservableProperty] private bool isReadOnly = arguments.IsReadOnly;

        partial void OnIsReadOnlyChanged(bool value)
        {
            IsReadOnlyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
