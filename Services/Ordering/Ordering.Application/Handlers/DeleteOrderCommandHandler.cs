using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.IRepositories;

namespace Ordering.Application.Handlers;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger, IOrderItemRepository orderItemRepository)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);
        if (orderToDelete is null)
            throw new OrderNotFoundException((request.Id));

        //if (orderToDelete == null)
        //{
        //    throw new OrderNotFoundException(nameof(Order), request.Id);
        //}

        if (orderToDelete != null)
            foreach (var item in orderToDelete.Items)
            {
                await _orderItemRepository.DeleteAsync(item);
            }

        await _orderRepository.DeleteAsync(orderToDelete);
        _logger.LogInformation($"Order with Id {request.Id} is deleted successfully.");

        return Unit.Value;
    }
}