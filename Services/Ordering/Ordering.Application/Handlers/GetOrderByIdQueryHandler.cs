using AutoMapper;
using MediatR;
using Ordering.Application.Exceptions;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.IRepositories;

namespace Ordering.Application.Handlers;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);
        if (order is null)
            throw new OrderNotFoundException(request.Id);
        return _mapper.Map<OrderResponse>(order);
    }
}