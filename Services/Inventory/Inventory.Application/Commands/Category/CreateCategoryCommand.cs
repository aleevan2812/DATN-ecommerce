using Inventory.Core.Dtos.Category;
using MediatR;

namespace Inventory.Application.Commands;
public record CreateCategoryCommand(
    string Name,
    int DisplayOrder
    ) : IRequest<CategoryDTO>;