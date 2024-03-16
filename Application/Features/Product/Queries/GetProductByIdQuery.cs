using Application.Core.Abstractions.Messaging;

namespace Application.Features.Product.Queries;
public sealed record GetProductByIdQuery(int Id) : IQuery<ProductDetailsDto>;

public record ProductDetailsDto(
    int Id,
    string Name,
    MoneyDto Price,
    string ShortDescription,
    string Description,
    DateTime CreatedDate
    );

