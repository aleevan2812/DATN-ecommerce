using Catalog.Application.Commands;
using Catalog.Application.Exceptions;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<UpdateProductHandler> _logger;

    public UpdateProductHandler(IProductRepository productRepository, ILogger<UpdateProductHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productToUpdate = await _productRepository.GetProduct(request.Id);

        if (productToUpdate == null)
            throw new ProductNotFoundException(request.Id);

        var productEntity = await _productRepository.UpdateProduct(new Product
        {
            Id = request.Id,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Name = request.Name,
            Price = request.Price,
            Summary = request.Summary,
            Brands = request.Brands,
            Types = request.Types
        });

        _logger.LogInformation($"Order {productToUpdate.Id} is successfully updated");

        return productEntity;
    }
}