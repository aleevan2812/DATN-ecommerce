using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.GrpcService;
using Ordering.Core.Entities;
using Ordering.Core.IRepositories;

namespace Ordering.Application.Handlers;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, string>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;
    private readonly DiscountGrpcService _discountGrpcService;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CheckoutOrderCommandHandler> logger, DiscountGrpcService discountGrpcService)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
        _discountGrpcService = discountGrpcService;
    }

    public async Task<string> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = _mapper.Map<Order>(request);

        var itemIds = request?.Items.Select(u => u.ProductId).Distinct().ToList();

        decimal totalDiscount = 0;
        foreach (var itemId in itemIds)
        {
            var coupon = await _discountGrpcService.GetDiscount(itemId);
            if (coupon != null && coupon.Quantity > 0)
            {
                totalDiscount += (decimal)coupon.Amount;
                _ = _discountGrpcService.ReduceDiscount(coupon.Id);
            }
        }
        orderEntity.TotalDiscount = totalDiscount;

        decimal totalPrice = 0;
        foreach (var item in orderEntity?.Items)
        {
            totalPrice += item.Price * item.Quantity;
        }
        orderEntity.TotalPrice = totalPrice > orderEntity.TotalDiscount ? totalPrice - orderEntity.TotalDiscount : 0;

        var generatedOrder = await _orderRepository.AddAsync(orderEntity);
        _logger.LogInformation(($"Order {generatedOrder} successfully created."));
        return generatedOrder.Id;
    }
}