using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands;

public class UpdateDiscountCommand : IRequest<CouponModel>
{
    public string Id { get; set; }
    public string ProductId { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public int Quantity { get; set; }
}