using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Grpc.Protos;
using EventBus.Messages.Events;

namespace Discount.Application.Mappers;

public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<Coupon, CouponModel>().ReverseMap();
        CreateMap<Coupon, UpdateDiscountCommand>().ReverseMap();
        CreateMap<ShoppingCartItem, UpdateDiscountCommand>().ReverseMap();
    }
}