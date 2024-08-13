using System.Net;

namespace Inventory.Application.Exceptions;

public class ProductNotFoundException : BaseException
{
    public ProductNotFoundException(Guid id)
        : base($"Product with id {id.ToString()} not found", HttpStatusCode.NotFound)
    {
    }
}