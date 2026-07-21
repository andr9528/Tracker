namespace Tracker.Module.Dining.Services.Import;

public sealed record DiningSpreadsheetRow(
    int RowNumber,
    DateOnly Date,
    string DishName,
    bool IsTakeAway,
    bool HasLeftovers,
    bool LeftoversEnoughForDinner,
    bool IsLeftovers,
    IReadOnlyCollection<string> IngredientNames,
    string? Notes);
