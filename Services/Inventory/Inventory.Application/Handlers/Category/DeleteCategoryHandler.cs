using AutoMapper;
using Inventory.Application.Commands;
using Inventory.Core.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Handlers;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateProductHandler> _logger;

    public DeleteCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateProductHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Category.GetAsync(u => u.Id == request.Id);

        //if (category == null)
        //{
        //    throw
        //}

        await _unitOfWork.Category.RemoveAsync(category);
        var result = await _unitOfWork.SaveAsync() > 0;

        return result;
    }
}