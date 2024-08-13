using AutoMapper;
using Inventory.Application.Commands;
using Inventory.Application.Exceptions;
using Inventory.Application.Services;
using Inventory.Core.Dtos.Product;
using Inventory.Core.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductDTO>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateProductHandler> _logger;
    private readonly CloudinaryService cloudinaryService;

    public UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateProductHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        cloudinaryService = new CloudinaryService();
    }

    public async Task<ProductDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Product.GetAsyncWithProductImages(u => u.Id == request.Id, false);

        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }

        _mapper.Map(request, product);

        if (request.ProductImages != null)
        {
            foreach (var image in request.ProductImages)
            {
                string imgUrl = await cloudinaryService.UploadImageAsync(image);
                product.ImageUrls.Add(imgUrl);
            }

            await _unitOfWork.Product.UpdateAsync(product);
            await _unitOfWork.SaveAsync();
        }

        return _mapper.Map<ProductDTO>(product);
    }
}