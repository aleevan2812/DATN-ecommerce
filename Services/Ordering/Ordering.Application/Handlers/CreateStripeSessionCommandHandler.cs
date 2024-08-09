using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.IRepositories;
using Stripe.Checkout;

namespace Ordering.Application.Handlers;

public class CreateStripeSessionCommandHandler : IRequestHandler<CreateStripeSessionCommand, CreateStripeSessionCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateStripeSessionCommandHandler> _logger;

    public CreateStripeSessionCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CreateStripeSessionCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateStripeSessionCommand> Handle(CreateStripeSessionCommand request, CancellationToken cancellationToken)
    {
        var options = new SessionCreateOptions
        {
            SuccessUrl = request.ApprovedUrl,
            CancelUrl = request.CancelUrl,
            LineItems = new List<SessionLineItemOptions>(),
            Mode = "payment",
        };

        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        if (order is null)
        {
            throw new OrderNotFoundException(request.OrderId);
        }

        //foreach (var item in order.Items)
        //{
        //    var sessionLineItem = new SessionLineItemOptions
        //    {
        //        PriceData = new SessionLineItemPriceDataOptions
        //        {
        //            UnitAmount = (long)(item.Price * 100), // $20.99 -> 2099
        //            Currency = "usd",
        //            ProductData = new SessionLineItemPriceDataProductDataOptions
        //            {
        //                Name = item.ProductName
        //            }
        //        },
        //        Quantity = item.Quantity,
        //    };

        //    options.LineItems.Add(sessionLineItem);
        //}

        options.LineItems.Add(new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmount = (long)(order.TotalPrice * 100), // $20.99 -> 2099
                Currency = "usd",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = $"Pay for order: {order.Id} with {order.Items.Count()} items: {string.Join(" and ", order.Items.Select(u => u.ProductName).ToList())}"
                }
            },
            Quantity = 1,
        });

        var service = new SessionService();

        Session session = service.Create(options);
        request.StripeSessionUrl = session.Url;

        order.StripeSessionId = session.Id;

        await _orderRepository.UpdateAsync(order);

        return request;
    }
}