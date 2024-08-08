using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers;

public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, CouponModel>
{
    private readonly IDiscountRepository _discountRepository;

    public GetDiscountByIdQueryHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<CouponModel> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        var coupon = await _discountRepository.GetDiscountById(request.Id);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Discount with the Id = {request.Id} not found"));
        }
        //TODO: Exercise Follow Product Mapper kind of example
        var couponModel = new CouponModel
        {
            Id = coupon.Id,
            Amount = coupon.Amount,
            Description = coupon.Description,
            ProductId = coupon.ProductId,
            Quantity = coupon.Quantity
        };
        return couponModel;
    }
}