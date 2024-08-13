using AutoMapper;
using Inventory.Application.Queries;
using Inventory.Core.Dtos.Category;
using Inventory.Core.IRepository;
using MediatR;

namespace Inventory.Application.Handlers;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDTO>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDTO> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Category.GetAsync(u => u.Id == request.Id);

        //if (category == null)
        //{
        //    throw
        //}

        return _mapper.Map<CategoryDTO>(category);
    }
}