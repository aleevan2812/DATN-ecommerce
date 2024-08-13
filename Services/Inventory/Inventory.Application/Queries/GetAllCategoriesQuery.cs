using Inventory.Core.Dtos.Category;
using MediatR;

namespace Inventory.Application.Queries;
public record GetAllCategoriesQuery(
    string? Search,
    int PageSize = 10,
    int PageNumber = 1
    ) : IRequest<IEnumerable<CategoryDTO>>;