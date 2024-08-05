using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.IRepositories;

using MediatR;

namespace Basket.Application.Handlers;

public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;

    public CreateShoppingCartCommandHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
    {
        _basketRepository = basketRepository;
        _discountGrpcService = discountGrpcService;
    }

    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var itemIds = request?.Items.Select(u => u.ProductId).Distinct().ToList();
        decimal totalDiscount = 0;
        if (itemIds is not null)
            foreach (var itemId in itemIds)
            {
                var coupon = await _discountGrpcService.GetDiscount(itemId);
                if (coupon != null && coupon.Quantity > 0)
                    totalDiscount += (decimal)coupon.Amount;
            }

        var shoppingCart = await _basketRepository.UpdateBasket(new ShoppingCart
        {
            UserName = request.UserName,
            Items = request.Items,
            TotalDiscount = totalDiscount
        });
        var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        return shoppingCartResponse;
    }
}