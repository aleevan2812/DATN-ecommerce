using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Queries;

public class GetDiscountByIdQuery : IRequest<CouponModel>
{
    public int Id { get; set; }

    public GetDiscountByIdQuery(int id)
    {
        Id = id;
    }
}