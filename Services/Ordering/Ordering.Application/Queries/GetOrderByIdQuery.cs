using MediatR;
using Ordering.Application.Responses;

namespace Ordering.Application.Queries;

public class GetOrderByIdQuery : IRequest<OrderResponse>
{
    public string Id { get; set; }

    public GetOrderByIdQuery(string id)
    {
        Id = id;
    }
}