using AutoMapper;
using Basket.Application.Responses;
using Basket.Core.Entities;
using EventBus.Messages.Events;

namespace Basket.Application.Mappers;

public class BasketMappingProfile : Profile
{
    public BasketMappingProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartResponse>().ReverseMap();
        CreateMap<Basket.Core.Entities.ShoppingCartItem, ShoppingCartItemResponse>().ReverseMap();

        CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        CreateMap<EventBus.Messages.Events.ShoppingCartItem, ShoppingCartItemResponse>().ReverseMap();
        CreateMap<ItemsBasketCheckoutEvent, ShoppingCartResponse>().ReverseMap();
    }
}