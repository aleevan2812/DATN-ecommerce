using AutoMapper;
using Inventory.Application.Commands;
using Inventory.Core.Dtos.Category;
using Inventory.Core.Entities;
using Inventory.Core.IRepository;
using MediatR;

namespace Inventory.Application.Handlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDTO>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDTO> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Category.GetAsync(u => u.Name.ToLower() == request.Name.ToLower()) != null)
        {
            //ModelState.AddModelError("ErrorMessages", "Category already exists!");
            //return BadRequest(ModelState);
        }

        //if (createDto == null)
        //    return BadRequest(createDto);

        var category = _mapper.Map<Category>(request);

        await _unitOfWork.Category.CreateAsync(category);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<CategoryDTO>(category);
    }
}