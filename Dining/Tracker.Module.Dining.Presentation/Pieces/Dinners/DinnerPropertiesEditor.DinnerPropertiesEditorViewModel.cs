using CommunityToolkit.Mvvm.ComponentModel;

namespace Tracker.Module.Dining.Presentation.Pieces.Dinners;

internal sealed partial class DinnerPropertiesEditor
{
    internal sealed partial class DinnerPropertiesEditorViewModel(DinnerPropertiesEditorArguments arguments)
        : ObservableObject
    {
        internal DinnerPropertiesEditorArguments Arguments { get; } = arguments;

        public event EventHandler? IsTakeAwayChanged;
        public event EventHandler? HasLeftoversChanged;
        public event EventHandler? LeftoversEnoughForDinnerChanged;
        public event EventHandler? IsLeftoversChanged;
        public event EventHandler? IsEatenOutChanged;
        public event EventHandler? IsReadyMadeDishChanged;
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

        partial void OnIsTakeAwayChanged(bool value)
        {
            IsTakeAwayChanged?.Invoke(this, EventArgs.Empty);
        }

        partial void OnHasLeftoversChanged(bool value)
        {
            HasLeftoversChanged?.Invoke(this, EventArgs.Empty);
        }

        partial void OnLeftoversEnoughForDinnerChanged(bool value)
        {
            LeftoversEnoughForDinnerChanged?.Invoke(this, EventArgs.Empty);
        }

        partial void OnIsLeftoversChanged(bool value)
        {
            IsLeftoversChanged?.Invoke(this, EventArgs.Empty);
        }

        partial void OnIsEatenOutChanged(bool value)
        {
            IsEatenOutChanged?.Invoke(this, EventArgs.Empty);
        }

        partial void OnIsReadyMadeDishChanged(bool value)
        {
            IsReadyMadeDishChanged?.Invoke(this, EventArgs.Empty);
        }

        partial void OnIsReadOnlyChanged(bool value)
        {
            IsReadOnlyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
