using Inventory.Core.Dtos.Product;
using MediatR;

namespace Inventory.Application.Queries;
public record GetAllProductsQuery(
    string? Search,
    int PageSize = 10,
    int PageNumber = 1
    ) : IRequest<IEnumerable<ProductDTO>>;