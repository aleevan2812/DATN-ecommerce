using AutoMapper;
using Inventory.Application.Queries;
using Inventory.Core.Dtos.Category;
using Inventory.Core.Entities;
using Inventory.Core.IRepository;
using MediatR;

namespace Inventory.Application.Handlers;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDTO>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Category> categories;
        categories = await _unitOfWork.Category.GetAllAsync(pageSize: request.PageSize, pageNumber: request.PageNumber);

        // use search
        if (!string.IsNullOrEmpty(request.Search))
            categories = categories.Where(u => u.Name.ToLower().Contains(request.Search));

        return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
    }
}