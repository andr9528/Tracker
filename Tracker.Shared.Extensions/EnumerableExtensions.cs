using System.Collections;

namespace Tracker.Shared.Extensions;

public static class EnumerableExtensions
{
    public static void ReplaceItems<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        ReplaceItemsUsing(collection.Clear, collection.Add, items);
    }

    public static void ReplaceItems(this IList list, IEnumerable items)
    {
        ReplaceItemsUsing(list.Clear, item => list.Add(item), items.Cast<object>());
    }

    private static void ReplaceItemsUsing<T>(Action clear, Action<T> add, IEnumerable<T> items)
    {
        clear();

        foreach (T item in items)
        {
            add(item);
        }
    }
}
