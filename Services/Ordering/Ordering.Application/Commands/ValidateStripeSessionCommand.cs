using MediatR;
using Ordering.Core.Entities;

namespace Ordering.Application.Commands;

public record ValidateStripeSessionCommand(
    string orderId
    ) : IRequest<OrderDto>;