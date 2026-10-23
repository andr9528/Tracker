using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI.UI.Controls;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntityDataGrid<TItem, TColumn>
{
    internal sealed class EntityDataGridViewModel(EntityDataGridArguments arguments) : ObservableObject
    {
        public EntityDataGridArguments Arguments { get; } = arguments;
        public DataGrid DataGrid { get; set; } = null!;
        public int SelectedId { get; set; } = arguments.InitialSelectedId;
        public bool IsUpdatingItems { get; set; }
    }
}
