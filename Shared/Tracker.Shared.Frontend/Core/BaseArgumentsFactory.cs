using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Data;
using Tracker.Shared.Abstraction.Interfaces.Frontend;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Pieces;

namespace Tracker.Shared.Frontend.Core;

public abstract class BaseArgumentsFactory(IUiDispatcher uiDispatcher)
{
    protected readonly IUiDispatcher uiDispatcher = uiDispatcher;

    public EntityDataGrid<TItem, TColumn>.EntityDataGridArguments CreateEntityDataGridArguments<TItem, TColumn>(
        ObservableCollection<TItem> items, Func<TColumn, string> getBindingPath,
        Func<Task<IEnumerable<TItem>>> getItems, Func<TColumn, IValueConverter?>? getColumnConverter = null,
        Func<TColumn, bool>? includeColumn = null) where TItem : class where TColumn : struct, Enum
    {
        return new EntityDataGrid<TItem, TColumn>.EntityDataGridArguments(items, getBindingPath, getItems, uiDispatcher,
            getColumnConverter, includeColumn);
    }


    public NullableBooleanOptionBar.NullableBooleanOptionBarArguments CreateNullableBooleanOptionBarArguments(
        string header, bool? initialValue = null)
    {
        return new NullableBooleanOptionBar.NullableBooleanOptionBarArguments(header, initialValue);
    }

    public EntitySelectionPiece<T>.EntitySelectionPieceArguments CreateEntitySelectionPieceArguments<T>(
        string header, ISuppliedEntityGrid<T> availableGrid, ISuppliedEntityGrid<T> suppliedGrid) where T : IEntity
    {
        return new EntitySelectionPiece<T>.EntitySelectionPieceArguments(header, availableGrid, suppliedGrid);
    }


    public EntitySelectorPiece<T>.EntitySelectorPieceArguments CreateEntitySelectorPieceArguments<T>(
        string header, IEntitySelectionGrid<T> grid, Func<T, string> selectionTextFactory,
        string emptySelectionText = "None selected") where T : IEntity
    {
        return new EntitySelectorPiece<T>.EntitySelectorPieceArguments(header, grid, selectionTextFactory,
            emptySelectionText);
    }
}
