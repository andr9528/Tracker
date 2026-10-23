using CommunityToolkit.WinUI.UI.Controls;
using Tracker.Shared.Extensions;
using Tracker.Shared.Frontend.Core;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntityDataGrid<TItem, TColumn>
{
    internal sealed class EntityDataGridLogic(EntityDataGridViewModel viewModel)
        : BaseLogic<EntityDataGridViewModel>(viewModel), IEntityDataGridApi
    {
        internal void SortColumnClicked(object? sender, DataGridColumnEventArgs e)
        {
            e.Column.SortDirection = e.Column.SortDirection switch
            {
                null => DataGridSortDirection.Ascending,
                DataGridSortDirection.Ascending => DataGridSortDirection.Descending,
                var _ => null,
            };

            foreach (DataGridColumn column in ViewModel.DataGrid.Columns)
            {
                if (column != e.Column)
                {
                    column.SortDirection = null;
                }
            }

            ViewModel.Arguments.Items.ReplaceItems(ApplyCurrentSort(ViewModel.Arguments.Items).ToList());
        }

        /// <inheritdoc />
        public DataGrid DataGrid => ViewModel.DataGrid;

        /// <inheritdoc />
        IEnumerable<TItem> IEntityDataGridApi.ApplyCurrentSort(IEnumerable<TItem> items)
        {
            return ApplyCurrentSort(items);
        }

        internal IEnumerable<TItem> ApplyCurrentSort(IEnumerable<TItem> items)
        {
            DataGridColumn? column = ViewModel.DataGrid.Columns.FirstOrDefault(x => x.SortDirection is not null);

            if (column?.Tag is not string bindingPath)
            {
                return items;
            }

            return column.SortDirection switch
            {
                DataGridSortDirection.Ascending => items.OrderBy(x => GetPropertyValue(x, bindingPath)),
                DataGridSortDirection.Descending => items.OrderByDescending(x => GetPropertyValue(x, bindingPath)),
                var _ => items,
            };
        }

        private static object? GetPropertyValue(object item, string propertyPath)
        {
            object? value = item;

            foreach (string propertyName in propertyPath.Split('.'))
            {
                if (value is null)
                {
                    return null;
                }

                value = value.GetType().GetProperty(propertyName)?.GetValue(value);
            }

            return value;
        }
    }
}
