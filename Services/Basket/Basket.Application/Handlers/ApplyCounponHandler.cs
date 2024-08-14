using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.IRepositories;

using MediatR;

namespace Basket.Application.Handlers;

public class ApplyCounponHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;

    public ApplyCounponHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
    {
        _basketRepository = basketRepository;
        _discountGrpcService = discountGrpcService;
    }

    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var shoppingCart = await _basketRepository.UpdateBasket(new ShoppingCart
        {
            UserName = request.UserName,
            Items = request.Items,
        });
        var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);

        // calculate totalPrice
        decimal totalPrice = 0;
        foreach (var item in shoppingCartResponse.Items)
        {
            var itemTotalPrice = item.Price * item.Quantity;
            var coupon = await _discountGrpcService.GetDiscount(item.CouponCode);
            decimal discount = 0;
            if (coupon != null && coupon.ProductId == item.ProductId && coupon.Quantity > 0)
                discount = coupon.Amount;
            totalPrice = totalPrice + (itemTotalPrice > discount ? itemTotalPrice - discount : 0);
        }
        shoppingCartResponse.TotalPrice = totalPrice;

        return shoppingCartResponse;
    }
}