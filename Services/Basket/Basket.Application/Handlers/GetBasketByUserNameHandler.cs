using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.IRepositories;
using MediatR;

namespace Basket.Application.Handlers;

public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;

    public GetBasketByUserNameHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
    {
        _basketRepository = basketRepository;
        _discountGrpcService = discountGrpcService;
    }

    public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        var shoppingCart = await _basketRepository.GetBasket(request.UserName);
        if (shoppingCart != null)
            shoppingCart.TotalDiscount = 0;

        var itemIds = shoppingCart?.Items.Select(u => u.ProductId).Distinct().ToList();

        if (itemIds is not null)
            foreach (var itemId in itemIds)
            {
                var coupon = await _discountGrpcService.GetDiscount(itemId);
                if (coupon != null && coupon.Quantity > 0)
                    shoppingCart.TotalDiscount += coupon.Amount;
            }

        var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        return shoppingCartResponse;
    }
}