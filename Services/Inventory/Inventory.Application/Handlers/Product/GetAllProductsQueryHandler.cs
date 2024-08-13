using AutoMapper;
using Inventory.Application.Queries;
using Inventory.Core.Dtos.Product;
using Inventory.Core.Entities;
using Inventory.Core.IRepository;
using MediatR;

namespace Inventory.Application.Handlers;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Product> products = await _unitOfWork.Product.GetAllAsyncWithProductImages(
                includeProperties: "Category",
                pageSize: request.PageSize, pageNumber: request.PageNumber);

        // use search
        if (!string.IsNullOrEmpty(request.Search))
            products = products.Where(u => u.Title.ToLower().Contains(request.Search));

        var res = _mapper.Map<List<ProductDTO>>(products);
        return res;
    }
}