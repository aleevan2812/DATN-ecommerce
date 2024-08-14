using AutoMapper;
using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;

namespace Discount.API.EventBusConsumer;

public class ItemsBasketOrderingConsumer : IConsumer<ItemsBasketCheckoutEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<ItemsBasketOrderingConsumer> _logger;

    public ItemsBasketOrderingConsumer(IMediator mediator, IMapper mapper, ILogger<ItemsBasketOrderingConsumer> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ItemsBasketCheckoutEvent> context)
    {
        using var scope = _logger.BeginScope($"Consuming ItemsBasketCheckoutEvent for {context.Message.CorrelationId}, for {context.Message.UserName} user");

        _logger.LogInformation($"---> Consuming ItemsBasketCheckoutEvent for {context.Message.CorrelationId}, for {context.Message.UserName} user");

        foreach (var item in context.Message.Items)
        {
            // init Query
            var query = new GetDiscountByProductIdQuery(item.ProductId);
            // get Coupon from Db
            var result = _mapper.Map<Coupon>(await _mediator.Send(query));

            var command = _mapper.Map<UpdateDiscountCommand>(result);
            command.Quantity--;
            await _mediator.Send(command);
        }

        _logger.LogInformation($"---> ItemsBasketCheckoutEvent completed!!!");
    }
}