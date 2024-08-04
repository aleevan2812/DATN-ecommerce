using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Queries;

public class GetDiscountQuery : IRequest<CouponModel>
{
    public string ProductId { get; set; }

    public GetDiscountQuery(string productId)
    {
        ProductId = productId;
    }
}