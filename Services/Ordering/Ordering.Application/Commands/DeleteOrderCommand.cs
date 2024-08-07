using MediatR;

namespace Ordering.Application.Commands;

public class DeleteOrderCommand : IRequest<Unit>
{
    public string Id { get; set; }
}