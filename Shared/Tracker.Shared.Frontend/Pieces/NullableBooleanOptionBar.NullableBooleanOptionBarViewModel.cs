using CommunityToolkit.Mvvm.ComponentModel;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class NullableBooleanOptionBar
{
    public sealed partial class NullableBooleanOptionBarViewModel : ObservableObject
    {
        [ObservableProperty] private string header = string.Empty;

        [ObservableProperty] private bool? selectedValue;

        public NullableBooleanOptionBarViewModel(NullableBooleanOptionBarArguments arguments)
        {
            Arguments = arguments;
            Header = arguments.Header;
            SelectedValue = arguments.InitialValue;
        }

        public NullableBooleanOptionBarArguments Arguments { get; }

        public RadioButton YesButton { get; set; } = null!;

        public RadioButton NoButton { get; set; } = null!;

        public RadioButton EitherButton { get; set; } = null!;
        public event EventHandler? SelectionChanged;

        partial void OnSelectedValueChanged(bool? value)
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
