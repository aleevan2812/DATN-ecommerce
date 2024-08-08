using Discount.Grpc.Protos;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.GrpcService;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;
    private readonly ILogger<DiscountGrpcService> _logger;
    private readonly IConfiguration _config;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient, ILogger<DiscountGrpcService> logger, IConfiguration config)
    {
        _discountProtoServiceClient = discountProtoServiceClient;
        _logger = logger;
        _config = config;
    }

    public async Task<CouponModel> GetDiscount(string productId)
    {
        _logger.LogInformation("Calling GRPC Service from Basket Client at GetDiscount .");

        // c2
        //var channel = GrpcChannel.ForAddress((_config.GetSection("GrpcSettings:DiscountUrl").Value).ToString());
        //var client = new DiscountProtoService.DiscountProtoServiceClient(channel);

        var discountRequest = new GetDiscountRequest { ProductId = productId };
        try
        {
            // c2
            //return await client.GetDiscountAsync(discountRequest);

            return await _discountProtoServiceClient.GetDiscountAsync(discountRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not call GRPC Server. This is call from Ordering Client at GetDiscount");
            return null;
        }
    }

    public async Task<Empty> ReduceDiscount(int id)
    {
        _logger.LogInformation("Calling GRPC Service from Basket Client at ReduceDiscount .");

        var reduceRequest = new ReduceDiscountRequest { Id = id };
        try
        {
            _ = _discountProtoServiceClient.ReduceDiscountAsync(reduceRequest);
            return await Task.FromResult(new Empty());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not call GRPC Server. This is call from Ordering Client at ReduceDiscount");
            return await Task.FromResult(new Empty());
        }
    }
}