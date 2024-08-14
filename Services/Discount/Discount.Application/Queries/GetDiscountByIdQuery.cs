using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Queries;

public class GetDiscountByIdQuery : IRequest<CouponModel>
{
    public string Id { get; set; }

    public GetDiscountByIdQuery(string id)
    {
        Id = id;
    }
}