using Domain.Common;

namespace Domain.Customers;
public class SearchHistory : BaseEntity
{
    public string SearchQuery { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    public string UserId { get; set; }

    public SearchHistory(string searchQuery, DateTime timestamp, string userId, string? sortColumn, string? sortOrder)
    {
        SearchQuery = searchQuery;
        Timestamp = timestamp;
        UserId = userId;
        SortColumn = sortColumn;
        SortOrder = sortOrder;
    }

    public static SearchHistory Create(
        string SearchQuery,
        DateTime Timestamp,
        string userId,
        string? SortColumn,
        string? SortOrder)
    {
        var searchHistory = new SearchHistory(
            SearchQuery,
            Timestamp,
            userId,
            SortColumn,
            SortOrder);

        return searchHistory;
    }
}
