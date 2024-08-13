using Common.Logging.Correlation;
using Inventory.Application.Commands;
using Inventory.Application.Queries;
using Inventory.Core.Dtos.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers;

public class ProductController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductController> _logger;
    private readonly ICorrelationIdGenerator _correlationIdGenerator;

    public ProductController(IMediator mediator, ILogger<ProductController> logger, ICorrelationIdGenerator correlationIdGenerator)
    {
        _mediator = mediator;
        _logger = logger;
        _correlationIdGenerator = correlationIdGenerator;
        _logger.LogInformation("CorrelationId {correlationId}:", _correlationIdGenerator.Get());
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts([FromQuery] GetAllProductsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProductById(Guid id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> CreateProduct([FromForm] CreateProductCommand cmd)
    {
        var result = await _mediator.Send(cmd);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<ProductDTO>> UpdateProduct([FromForm] UpdateProductCommand cmd)
    {
        var result = await _mediator.Send(cmd);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<ActionResult<ProductDTO>> DeleteProduct(Guid id)
    {
        var cmd = new DeleteProductCommand(id);
        var result = await _mediator.Send(cmd);
        return Ok(result);
    }
}