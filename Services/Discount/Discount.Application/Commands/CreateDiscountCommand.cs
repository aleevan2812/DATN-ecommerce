using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands;

public class CreateDiscountCommand : IRequest<CouponModel>
{
    public string ProductId { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public int Quantity { get; set; }
}