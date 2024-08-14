using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Queries;

public class GetDiscountByProductIdQuery : IRequest<CouponModel>
{
    public string ProductId { get; set; }

    public GetDiscountByProductIdQuery(string productId)
    {
        ProductId = productId;
    }
}