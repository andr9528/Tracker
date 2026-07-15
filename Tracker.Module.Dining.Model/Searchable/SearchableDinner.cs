using Tracker.Module.Dining.Abstraction.Searchable;

namespace Tracker.Module.Dining.Model.Searchable;

public class SearchableDinner : ISearchableDinner
{
    #region Implementation of ISearchable

    /// <inheritdoc />
    public int Id { get; set; }

    #endregion

    #region Implementation of ISearchableDinner

    /// <inheritdoc />
    public DateOnly Date { get; set; }

    /// <inheritdoc />
    public string? Notes { get; set; }

    /// <inheritdoc />
    public bool IsTakeAway { get; set; }

    /// <inheritdoc />
    public bool HasLeftovers { get; set; }

    /// <inheritdoc />
    public bool LeftoversEnoughForDinner { get; set; }

    /// <inheritdoc />
    public bool IsLeftovers { get; set; }

    /// <inheritdoc />
    public int DishId { get; set; }

    #endregion
}
