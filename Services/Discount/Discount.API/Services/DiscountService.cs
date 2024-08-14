using Common.Logging.Correlation;
using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;

namespace Discount.API.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DiscountService> _logger;
    private readonly ICorrelationIdGenerator _correlationIdGenerator;

    public DiscountService(IMediator mediator, ILogger<DiscountService> logger, ICorrelationIdGenerator correlationIdGenerator)
    {
        _mediator = mediator;
        _logger = logger;
        _correlationIdGenerator = correlationIdGenerator;
        _logger.LogInformation("CorrelationId {correlationId}:", _correlationIdGenerator.Get());
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var query = new GetDiscountByIdQuery(request.Id);
        var result = await _mediator.Send(query);
        _logger.LogInformation($"Discount is retrieved for the Id-CouponCode: {request.Id} for ProductId {result.ProductId} and Amount : {result.Amount} and Quantity : {result.Quantity}");
        return result;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var cmd = new CreateDiscountCommand
        {
            ProductId = request.Coupon.ProductId,
            Amount = request.Coupon.Amount,
            Description = request.Coupon.Description
        };
        var result = await _mediator.Send(cmd);
        _logger.LogInformation($"Discount is successfully created for the Product Id: {result.ProductId}");
        return result;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var cmd = new UpdateDiscountCommand
        {
            Id = request.Coupon.Id,
            ProductId = request.Coupon.ProductId,
            Amount = request.Coupon.Amount,
            Description = request.Coupon.Description,
            Quantity = request.Coupon.Quantity
        };
        var result = await _mediator.Send(cmd);
        _logger.LogInformation($"Discount is successfully updated Product Name: {result.ProductId}");
        return result;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var cmd = new DeleteDiscountCommand(request.Id);
        var deleted = await _mediator.Send(cmd);
        var response = new DeleteDiscountResponse
        {
            Success = deleted
        };
        return response;
    }

    public override async Task<Empty> ReduceDiscount(ReduceDiscountRequest request, ServerCallContext context)
    {
        var query = new GetDiscountByIdQuery(request.Id);
        var coupon = await _mediator.Send(query);
        _logger.LogInformation($"Discount is reduced quantity for the Id: {coupon.Id} and Amount : {coupon.Amount} and Quantity : {coupon.Quantity}");

        var command = new UpdateDiscountCommand
        {
            Id = coupon.Id,
            ProductId = coupon.ProductId,
            Amount = coupon.Amount,
            Description = coupon.Description,
            Quantity = coupon.Quantity - 1
        };

        var reduced = await _mediator.Send(command);
        _logger.LogInformation($"Discount is reduced quantity for the Id: {reduced.Id} and Amount : {reduced.Amount} and Quantity : {reduced.Quantity}");
        return await Task.FromResult(new Empty());
    }
}