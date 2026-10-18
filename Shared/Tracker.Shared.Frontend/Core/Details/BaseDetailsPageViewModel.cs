using CommunityToolkit.Mvvm.ComponentModel;

namespace Tracker.Shared.Frontend.Core.Details;

public abstract partial class BaseDetailsPageViewModel : ObservableObject
{
    [ObservableProperty] private string cancelButtonText = "Back";
    [ObservableProperty] private bool canDelete;
    [ObservableProperty] private bool hasChanges;
    [ObservableProperty] private bool isEditing;
    [ObservableProperty] private string saveButtonText = "Okay";

    public Button DeleteButton { get; set; } = null!;
    public CheckBox EditCheckBox { get; set; } = null!;
    public Button SaveButton { get; set; } = null!;
    public Button CancelButton { get; set; } = null!;
}
