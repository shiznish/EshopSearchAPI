using Application.Core.Abstractions.Messaging;
using Application.Features.Product.Queries;

namespace Application.Features.User.Queries;
public record GetUserSearchHistoryQuery(string userId, int Page, int PageSize)
    : IQuery<PagedList<SearchHistoryDto>>;

public record SearchHistoryDto(
     string SearchQuery,
    DateTime Timestamp,
    string? SortColumn,
    string? SortOrder
);

