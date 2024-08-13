using MediatR;

namespace Inventory.Application.Commands;
public record DeleteProductCommand(
    Guid Id
    ) : IRequest<bool>;