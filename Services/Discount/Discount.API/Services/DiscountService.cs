using Common.Logging.Correlation;
using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
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
        var query = new GetDiscountQuery(request.ProductId);
        var result = await _mediator.Send(query);
        _logger.LogInformation($"Discount is retrieved for the Product Id: {request.ProductId} and Amount : {result.Amount} and Quantity : {result.Quantity}");
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
            Description = request.Coupon.Description
        };
        var result = await _mediator.Send(cmd);
        _logger.LogInformation($"Discount is successfully updated Product Name: {result.ProductId}");
        return result;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var cmd = new DeleteDiscountCommand(request.ProductId);
        var deleted = await _mediator.Send(cmd);
        var response = new DeleteDiscountResponse
        {
            Success = deleted
        };
        return response;
    }
}