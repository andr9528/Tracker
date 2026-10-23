using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using System.Collections;
using Tracker.Shared.Extensions;

namespace Tracker.Shared.Frontend.Factory;

public static class DataGridFactory
{
    public static DataGrid Create<TColumn>(
        IEnumerable<object> itemsSource, Func<TColumn, string> getBindingPath,
        Func<TColumn, IValueConverter?>? getColumnConverter = null, Func<TColumn, bool>? includeColumn = null)
        where TColumn : struct, Enum
    {
        var dataGrid = new DataGrid
        {
            AutoGenerateColumns = false,
            IsReadOnly = true,
            ItemsSource = itemsSource,
            CanUserSortColumns = true,
            Foreground = new SolidColorBrush(Colors.Black),
        };

        foreach (TColumn column in Enum.GetValues<TColumn>())
        {
            if (includeColumn is not null && !includeColumn(column))
            {
                continue;
            }

            string header = column.ToColumnHeader();
            string bindingPath = getBindingPath(column);
            IValueConverter? converter = getColumnConverter?.Invoke(column);

            dataGrid.Columns.Add(CreateTextColumn(header, bindingPath, converter));
        }

        return dataGrid;
    }

    private static DataGridTextColumn CreateTextColumn(
        string header, string bindingPath, IValueConverter? converter = null)
    {
        return new DataGridTextColumn
        {
            Header = header,
            MaxWidth = 500,
            CanUserSort = true,
            Tag = bindingPath,
            Binding = new Binding
            {
                Path = new PropertyPath(bindingPath),
                Converter = converter,
            },
        };
    }

    public static void Refresh(this DataGrid dataGrid)
    {
        IEnumerable? source = dataGrid.ItemsSource;

        dataGrid.ItemsSource = null;
        dataGrid.ItemsSource = source;
    }
}
