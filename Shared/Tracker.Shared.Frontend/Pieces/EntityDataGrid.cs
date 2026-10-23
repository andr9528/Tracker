using System.Collections.ObjectModel;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Data;
using Tracker.Shared.Frontend.Abstraction;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntityDataGrid<TItem, TColumn> : Border where TItem : class where TColumn : struct, Enum
{
    public EntityDataGrid(EntityDataGridArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);

        ViewModel = new EntityDataGridViewModel(arguments);
        Logic = new EntityDataGridLogic(ViewModel);
        var ui = new EntityDataGridUi(Logic, ViewModel);
        Child = ui.CreateContentGrid();
    }

    private EntityDataGridViewModel ViewModel { get; }
    private EntityDataGridLogic Logic { get; }

    public IEntityDataGridApi Api => Logic;

    public record EntityDataGridArguments(
        ObservableCollection<TItem> Items,
        Func<TColumn, string> GetBindingPath,
        Func<Task<IEnumerable<TItem>>> GetItems,
        IUiDispatcher UiDispatcher,
        Func<TItem, int> GetItemId,
        int InitialSelectedId = 0,
        Func<TColumn, IValueConverter?>? GetColumnConverter = null,
        Func<TColumn, bool>? IncludeColumn = null);

    public interface IEntityDataGridApi
    {
        DataGrid DataGrid { get; }

        IEnumerable<TItem> ApplyCurrentSort(IEnumerable<TItem> items);

        TItem? SelectedItem { get; }

        int SelectedId { get; set; }

        event EventHandler? SelectionChanged;

        Task Refresh();
    }
}
