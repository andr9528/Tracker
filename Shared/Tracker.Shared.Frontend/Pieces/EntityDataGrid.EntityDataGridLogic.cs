using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Controls;
using Tracker.Shared.Extensions;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Factory;

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

            UpdateItems(ApplyCurrentSort(ViewModel.Arguments.Items).ToList());
        }

        /// <inheritdoc />
        public DataGrid DataGrid => ViewModel.DataGrid;

        /// <inheritdoc />
        IEnumerable<TItem> IEntityDataGridApi.ApplyCurrentSort(IEnumerable<TItem> items)
        {
            return ApplyCurrentSort(items);
        }

        public TItem? SelectedItem => ViewModel.DataGrid.SelectedItem as TItem;

        public int SelectedId
        {
            get => ViewModel.SelectedId;
            set
            {
                int previousId = ViewModel.SelectedId;
                ViewModel.IsUpdatingItems = true;
                try
                {
                    SelectById(value);
                }
                finally
                {
                    ViewModel.IsUpdatingItems = false;
                }

                if (previousId != ViewModel.SelectedId)
                {
                    SelectionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler? SelectionChanged;

        internal void OnGridSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (ViewModel.IsUpdatingItems)
            {
                return;
            }

            int id = SelectedItem is { } item ? ViewModel.Arguments.GetItemId(item) : 0;
            if (ViewModel.SelectedId == id)
            {
                return;
            }

            ViewModel.SelectedId = id;
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public async Task Refresh()
        {
            var items = (await ViewModel.Arguments.GetItems()).ToList();
            var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

            bool enqueued = ViewModel.Arguments.UiDispatcher.TryEnqueue(() =>
            {
                try
                {
                    UpdateItems(ApplyCurrentSort(items).ToList());
                    completion.TrySetResult();
                }
                catch (Exception exception)
                {
                    completion.TrySetException(exception);
                }
            });

            if (!enqueued)
            {
                throw new InvalidOperationException("Could not enqueue DataGrid refresh on the UI dispatcher.");
            }

            await completion.Task;
        }

        private void UpdateItems(List<TItem> items)
        {
            int previousId = ViewModel.SelectedId;
            ViewModel.IsUpdatingItems = true;
            try
            {
                ViewModel.Arguments.Items.ReplaceItems(items);
                ViewModel.DataGrid.Refresh();
                SelectById(previousId);
            }
            finally
            {
                ViewModel.IsUpdatingItems = false;
            }

            if (previousId != ViewModel.SelectedId)
            {
                SelectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void SelectById(int id)
        {
            TItem? item = id == 0
                ? null
                : ViewModel.Arguments.Items.FirstOrDefault(x => ViewModel.Arguments.GetItemId(x) == id);
            ViewModel.DataGrid.SelectedItem = item;
            ViewModel.SelectedId = item is null ? 0 : id;
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
