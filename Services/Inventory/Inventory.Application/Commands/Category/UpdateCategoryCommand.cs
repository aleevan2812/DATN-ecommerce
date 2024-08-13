using Inventory.Core.Dtos.Category;
using MediatR;

namespace Inventory.Application.Commands;
public record UpdateCategoryCommand(
    Guid Id,
    string Name,
    int DisplayOrder
    ) : IRequest<CategoryDTO>;