using AutoMapper;
using Inventory.Application.Commands;
using Inventory.Core.Dtos.Category;
using Inventory.Core.Entities;
using Inventory.Core.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Handlers;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryDTO>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateCategoryHandler> _logger;

    public UpdateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateCategoryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CategoryDTO> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Category.GetAsync(u => u.Id == request.Id, false) == null)
        {
            //throw new CategoryNotFoundException(request.Id);
        }

        var category = _mapper.Map<Category>(request);

        await _unitOfWork.Category.UpdateAsync(category);
        await _unitOfWork.SaveAsync();

        _logger.LogInformation($"Category {category.Id} is successfully updated");

        return _mapper.Map<CategoryDTO>(category);
    }
}