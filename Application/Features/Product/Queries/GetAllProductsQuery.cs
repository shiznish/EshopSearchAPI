using Application.Core.Abstractions.Messaging;

namespace Application.Features.Product.Queries;
public record GetAllProductsQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IQuery<PagedList<ProductResponse>>;
public record ProductDto(
    int Id,
    string Name,
    string? ShortDescription,
    MoneyDto Price,
    string CategoryName
);

public record ProductResponse(
    int Id,
    string Name,
    string ShortDescription,
    string Currency,
    decimal Amount);
public record MoneyDto(
    string Currency,
    decimal Amount
);