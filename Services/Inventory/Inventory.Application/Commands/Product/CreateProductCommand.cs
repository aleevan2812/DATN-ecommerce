using Inventory.Core.Dtos.Product;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Inventory.Application.Commands;
public record CreateProductCommand(
     string Seller,
     string ISBN,
     string Title,
     string? Description,
     string Author,
     double? Price,
     int StockQuantity,
     Guid CategoryId,
     List<IFormFile>? ProductImages
    ) : IRequest<ProductDTO>;