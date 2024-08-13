using MediatR;

namespace Inventory.Application.Commands;
public record DeleteCategoryCommand(
    Guid Id
    ) : IRequest<bool>;