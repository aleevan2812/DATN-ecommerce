using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Application.GrpcService;
using Ordering.Core.Entities;
using Ordering.Core.IRepositories;
using Stripe;
using Stripe.Checkout;

namespace Ordering.Application.Handlers;

public class ValidateStripeSessionHandler : IRequestHandler<ValidateStripeSessionCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ValidateStripeSessionCommand> _logger;
    private readonly DiscountGrpcService _discountGrpcService;

    public ValidateStripeSessionHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<ValidateStripeSessionCommand> logger, DiscountGrpcService discountGrpcService)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
        _discountGrpcService = discountGrpcService;
    }

    public async Task<OrderDto> Handle(ValidateStripeSessionCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.orderId);
        if (order is null)
            throw new OrderNotFoundException(request.orderId);

        var service = new SessionService();
        Session session = service.Get(order.StripeSessionId);

        if (session is not null && session.Status == "complete")
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntentId = session.PaymentIntentId;
            if (paymentIntentId != null)
            {
                PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);
                order.PaymentIntentId = paymentIntent.Status == "succeeded" ? paymentIntent.Id : null;
            }

            order.Status = "Approved";

            await _orderRepository.UpdateAsync(order);
        }

        var orderDto = _mapper.Map<OrderDto>(order);

        return orderDto;
    }
}