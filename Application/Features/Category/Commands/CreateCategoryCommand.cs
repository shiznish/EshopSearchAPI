

using Application.Core.Abstractions.Messaging;

namespace Application.Features.Category.Commands;
public sealed record CreateCategoryCommand(string Name) : ICommand;
