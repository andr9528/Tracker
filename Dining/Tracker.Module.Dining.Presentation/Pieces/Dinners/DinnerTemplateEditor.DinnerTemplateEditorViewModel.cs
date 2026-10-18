using CommunityToolkit.Mvvm.ComponentModel;

namespace Tracker.Module.Dining.Presentation.Pieces.Dinners;

internal sealed partial class DinnerTemplateEditor
{
    internal sealed partial class DinnerTemplateEditorViewModel(DinnerTemplateEditorArguments arguments)
        : ObservableObject
    {
        [ObservableProperty] private bool isReadOnly = arguments.IsReadOnly;

        [ObservableProperty] private string name = arguments.DinnerTemplate?.Name ?? string.Empty;
        internal DinnerTemplateEditorArguments Arguments { get; } = arguments;

        internal TextBox NameTextBox { get; set; } = null!;

        internal DinnerPropertiesEditor PropertiesEditor { get; set; } = null!;

        public event EventHandler? IsReadOnlyChanged;
        public event EventHandler? NameChanged;

        partial void OnNameChanged(string value)
        {
            NameChanged?.Invoke(this, EventArgs.Empty);
        }

        partial void OnIsReadOnlyChanged(bool value)
        {
            IsReadOnlyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
