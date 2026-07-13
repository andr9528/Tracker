namespace Tracker.Shared.Extensions;

public static class StringExtensions
{
    public static string ScreamingSnakeCaseToTitleCase(this string input)
    {
        return string.Join(' ',
            input.Split('_', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => char.ToUpperInvariant(x[0]) + x[1..].ToLowerInvariant()));
    }

    public static string ToColumnHeader<TColumn>(this TColumn column) where TColumn : Enum
    {
        return column.ToString().ScreamingSnakeCaseToTitleCase();
    }
}
