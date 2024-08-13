using Inventory.Core.Dtos.Product;
using MediatR;

namespace Inventory.Application.Queries;
public record GetProductByIdQuery(
        Guid Id
        ) : IRequest<ProductDTO>;