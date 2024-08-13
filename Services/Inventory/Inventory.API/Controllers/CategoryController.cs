using Common.Logging.Correlation;
using Inventory.Application.Commands;
using Inventory.Application.Queries;
using Inventory.Core.Dtos.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers;

public class CategoryController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ILogger<CategoryController> _logger;
    private readonly ICorrelationIdGenerator _correlationIdGenerator;

    public CategoryController(IMediator mediator, ILogger<CategoryController> logger, ICorrelationIdGenerator correlationIdGenerator)
    {
        _mediator = mediator;
        _logger = logger;
        _correlationIdGenerator = correlationIdGenerator;
        _logger.LogInformation("CorrelationId {correlationId}:", _correlationIdGenerator.Get());
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories([FromQuery] GetAllCategoriesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDTO>> GetCategoryById(Guid id)
    {
        var query = new GetCategoryByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("CreateCategory")]
    public async Task<ActionResult<CategoryDTO>> CreateProduct(CreateCategoryCommand cmd)
    {
        var result = await _mediator.Send(cmd);
        return Ok(result);
    }

    [HttpPut("UpdateCategory")]
    public async Task<ActionResult<bool>> UpdateCategory(UpdateCategoryCommand cmd)
    {
        var result = await _mediator.Send(cmd);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CategoryDTO>> DeleteCategory(Guid id)
    {
        var cmd = new DeleteCategoryCommand(id);
        var result = await _mediator.Send(cmd);
        return Ok(result);
    }
}