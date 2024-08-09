using System.Net;

namespace Ordering.Application.Exceptions;

public class OrderNotFoundException : BaseException
{
    public OrderNotFoundException(string id)
        : base($"Order with id: {id} not found", HttpStatusCode.NotFound)
    {
    }
}