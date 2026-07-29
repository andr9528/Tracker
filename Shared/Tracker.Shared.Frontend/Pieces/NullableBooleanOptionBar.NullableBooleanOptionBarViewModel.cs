using CommunityToolkit.Mvvm.ComponentModel;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class NullableBooleanOptionBar
{
    public sealed partial class NullableBooleanOptionBarViewModel : ObservableObject
    {
        public NullableBooleanOptionBarArguments Arguments { get; }
        public event EventHandler? SelectionChanged;

        [ObservableProperty] private bool? selectedValue;

        [ObservableProperty] private string header = string.Empty;

        public RadioButton YesButton { get; set; } = null!;

        public RadioButton NoButton { get; set; } = null!;

        public RadioButton EitherButton { get; set; } = null!;

        public NullableBooleanOptionBarViewModel(NullableBooleanOptionBarArguments arguments)
        {
            Arguments = arguments;
            Header = arguments.Header;
            SelectedValue = arguments.InitialValue;
        }

        partial void OnSelectedValueChanged(bool? value)
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
