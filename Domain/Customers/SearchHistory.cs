using Domain.Common;

namespace Domain.Customers;
public class SearchHistory : BaseEntity
{
    public string SearchQuery { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public SearchHistory(string searchQuery, DateTime timestamp, string? sortColumn, string? sortOrder)
    {
        SearchQuery = searchQuery;
        Timestamp = timestamp;
        SortColumn = sortColumn;
        SortOrder = sortOrder;
        CustomerId = 1;
    }

    public static SearchHistory Create(
        string SearchQuery,
        DateTime Timestamp,
        string? SortColumn,
        string? SortOrder)
    {
        var searchHistory = new SearchHistory(
            SearchQuery,
            Timestamp,
            SortColumn,
            SortOrder);

        return searchHistory;
    }
}
