using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.IRepositories;

namespace Ordering.Application.Handlers;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
        if (orderToUpdate is null)
            throw new OrderNotFoundException(request.Id);

        //if (orderToUpdate == null)
        //{
        //    throw new OrderNotFoundException(nameof(Order), request.Id);
        //}

        orderToUpdate.FullName = request?.FullName;
        orderToUpdate.EmailAddress = request?.EmailAddress;
        orderToUpdate.AddressLine = request?.AddressLine;
        orderToUpdate.PhoneNumber = request?.PhoneNumber;
        orderToUpdate.Country = request?.Country;
        orderToUpdate.State = request?.State;
        orderToUpdate.ZipCode = request?.ZipCode;

        await _orderRepository.UpdateAsync(orderToUpdate);
        _logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated");

        return Unit.Value;
    }
}