using Inventory.Core.Dtos.Category;
using MediatR;

namespace Inventory.Application.Queries;
public record GetCategoryByIdQuery(
        Guid Id
        ) : IRequest<CategoryDTO>;