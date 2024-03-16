using Application.Core.Abstractions.Messaging;
using Domain.Products;

namespace Application.Features.Product.Commands;
public sealed record CreateProductCommand(string Name, string? ShortDescription, string? Description, Money Price, int CategoryId) : ICommand;
