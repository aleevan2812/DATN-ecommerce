using MediatR;

namespace Discount.Application.Commands;

public class DeleteDiscountCommand : IRequest<bool>
{
    public string ProductId { get; set; }

    public DeleteDiscountCommand(string productId)
    {
        ProductId = productId;
    }
}