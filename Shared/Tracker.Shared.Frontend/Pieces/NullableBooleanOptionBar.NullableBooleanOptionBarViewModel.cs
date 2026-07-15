using CommunityToolkit.Mvvm.ComponentModel;

namespace Tracker.Shared.Frontend.Pieces;

internal sealed partial class NullableBooleanOptionBar
{
    internal sealed partial class NullableBooleanOptionBarViewModel : ObservableObject
    {
        public NullableBooleanOptionBarArguments Arguments { get; }
        internal event EventHandler? SelectionChanged;

        [ObservableProperty] private bool? selectedValue = null;

        [ObservableProperty] private string header = string.Empty;

        public RadioButton YesButton { get; set; } = null!;

        public RadioButton NoButton { get; set; } = null!;

        public RadioButton AnyButton { get; set; } = null!;

        public NullableBooleanOptionBarViewModel(NullableBooleanOptionBarArguments arguments)
        {
            Arguments = arguments;
            Header = arguments.Header;
        }

        partial void OnSelectedValueChanged(bool? value)
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
