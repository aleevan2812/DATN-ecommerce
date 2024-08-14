using MediatR;

namespace Discount.Application.Commands;

public class DeleteDiscountCommand : IRequest<bool>
{
    public string Id { get; set; }

    public DeleteDiscountCommand(string id)
    {
        Id = id;
    }
}