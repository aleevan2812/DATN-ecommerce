using AutoMapper;
using Inventory.Application.Exceptions;
using Inventory.Application.Queries;
using Inventory.Core.Dtos.Product;
using Inventory.Core.IRepository;
using MediatR;

namespace Inventory.Application.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Product.GetAsyncWithProductImages(u => u.Id == request.Id);

        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }

        var res = _mapper.Map<ProductDTO>(product);
        return res;
    }
}