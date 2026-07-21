using ClosedXML.Excel;
using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Abstraction.Records;

namespace Tracker.Module.Dining.Services.Import;

public sealed class DiningSpreadsheetReader
{
    private const int FIRST_DATA_ROW = 3;

    private readonly ILogger<DiningSpreadsheetReader> logger;

    public DiningSpreadsheetReader(ILogger<DiningSpreadsheetReader> logger)
    {
        this.logger = logger;
    }

    public IReadOnlyCollection<DiningSpreadsheetRow> Read(Stream stream, ImportResult counters)
    {
        using var workbook = new XLWorkbook(stream);
        IXLWorksheet worksheet = workbook.Worksheets.First();

        return worksheet.RowsUsed().Where(x => x.RowNumber() >= FIRST_DATA_ROW).Select(x => ParseRow(x, counters))
            .Where(x => x is not null).Cast<DiningSpreadsheetRow>().ToList();
    }

    private DiningSpreadsheetRow? ParseRow(IXLRow row, ImportResult counters)
    {
        var date = ParseDate(row.Cell(2));
        string dishName = NormalizeName(row.Cell(3).GetString());

        if (!date.HasValue)
        {
            LogSkippedRow(row, "The date is missing or invalid.");
            counters.SkippedInvalidRows++;
            return null;
        }

        if (string.IsNullOrWhiteSpace(dishName))
        {
            LogSkippedRow(row, "The dish name is missing.");
            counters.SkippedInvalidRows++;
            return null;
        }

        return new DiningSpreadsheetRow(row.RowNumber(), date.Value, dishName, ParseBoolean(row.Cell(4)),
            ParseBoolean(row.Cell(5)), ParseBoolean(row.Cell(6)), ParseBoolean(row.Cell(7)),
            ParseIngredients(row.Cell(8)), NormalizeOptionalText(row.Cell(9).GetString()));
    }

    private DateOnly? ParseDate(IXLCell cell)
    {
        if (cell.TryGetValue<DateTime>(out DateTime dateTime))
        {
            return DateOnly.FromDateTime(dateTime);
        }

        if (cell.TryGetValue<double>(out double serialDate))
        {
            return DateOnly.FromDateTime(DateTime.FromOADate(serialDate));
        }

        return DateOnly.TryParse(cell.GetString(), out DateOnly parsedDate) ? parsedDate : null;
    }

    private bool ParseBoolean(IXLCell cell)
    {
        string value = cell.GetString().Trim();

        return value.Equals("Ja", StringComparison.OrdinalIgnoreCase);
    }

    private IReadOnlyCollection<string> ParseIngredients(IXLCell cell)
    {
        return cell.GetString().Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(NormalizeName).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private string NormalizeName(string value)
    {
        return value.Trim();
    }

    private string? NormalizeOptionalText(string value)
    {
        string normalized = value.Trim();

        return string.IsNullOrWhiteSpace(normalized) ? null : normalized;
    }

    private void LogSkippedRow(IXLRow row, string reason)
    {
        logger.LogWarning("Skipping dining spreadsheet row {RowNumber}. {Reason}", row.RowNumber(), reason);
    }
}
