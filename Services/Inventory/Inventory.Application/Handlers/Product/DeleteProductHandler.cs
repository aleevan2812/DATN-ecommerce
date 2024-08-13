using AutoMapper;
using Inventory.Application.Commands;
using Inventory.Application.Exceptions;
using Inventory.Core.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Handlers;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateProductHandler> _logger;

    public DeleteProductHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateProductHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Product.GetAsync(u => u.Id == request.Id);

        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }

        await _unitOfWork.Product.RemoveAsync(product);
        var result = await _unitOfWork.SaveAsync() > 0;

        return result;
    }
}