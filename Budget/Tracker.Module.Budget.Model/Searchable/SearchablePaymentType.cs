using Tracker.Module.Budget.Abstraction.Searchable;

namespace Tracker.Module.Budget.Model.Searchable;

public class SearchablePaymentType : ISearchablePaymentType
{
    #region Implementation of ISearchable

    /// <inheritdoc />
    public int Id { get; set; }

    #endregion

    #region Implementation of ISearchablePaymentType

    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public string Description { get; set; }

    #endregion
}
