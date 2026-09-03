using CommunityToolkit.Mvvm.ComponentModel;

namespace Tracker.Module.Dining.Presentation.Pieces.Dinners;

internal sealed partial class DinnerTemplateEditor
{
    internal sealed partial class DinnerTemplateEditorViewModel(DinnerTemplateEditorArguments arguments)
        : ObservableObject
    {
        internal DinnerTemplateEditorArguments Arguments { get; } = arguments;

        public event EventHandler? IsReadOnlyChanged;

        internal TextBox NameTextBox { get; set; } = null!;

        internal DinnerPropertiesEditor PropertiesEditor { get; set; } = null!;

        [ObservableProperty] private string name = arguments.DinnerTemplate?.Name ?? string.Empty;

        [ObservableProperty] private bool isReadOnly = arguments.IsReadOnly;

        partial void OnIsReadOnlyChanged(bool value)
        {
            IsReadOnlyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
