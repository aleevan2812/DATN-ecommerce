using AutoMapper;
using Inventory.Application.Commands;
using Inventory.Application.Services;
using Inventory.Core.Dtos.Product;
using Inventory.Core.Entities;
using Inventory.Core.IRepository;
using MediatR;

namespace Inventory.Application.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDTO>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CloudinaryService cloudinaryService;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        cloudinaryService = new CloudinaryService();
    }

    public async Task<ProductDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Product.GetAsync(u => u.ISBN.ToLower() == request.ISBN.ToLower()) != null)
        {
            //ModelState.AddModelError("ErrorMessages", "Product  already exists!");
            //return BadRequest(ModelState);
        }

        Product product = _mapper.Map<Product>(request);

        await _unitOfWork.Product.CreateAsync(product);
        await _unitOfWork.SaveAsync();
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