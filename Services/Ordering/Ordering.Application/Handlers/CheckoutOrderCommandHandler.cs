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
        //var items = request?.Items.se

        // calculate totalPrice
        decimal totalPrice = 0;
        foreach (var item in orderEntity.Items)
        {
            var itemTotalPrice = item.Price * item.Quantity;
            var coupon = await _discountGrpcService.GetDiscount(item.CouponCode);
            decimal discount = 0;
            if (coupon != null && coupon.ProductId == item.ProductId && coupon.Quantity > 0)
            {
                discount = coupon.Amount;
                _ = _discountGrpcService.ReduceDiscount(coupon.Id);
            }

            totalPrice = totalPrice + (itemTotalPrice > discount ? itemTotalPrice - discount : 0);
        }
        orderEntity.TotalPrice = totalPrice;

        var generatedOrder = await _orderRepository.AddAsync(orderEntity);
        _logger.LogInformation(($"Order {generatedOrder} successfully created."));
        return generatedOrder.Id;
    }
}