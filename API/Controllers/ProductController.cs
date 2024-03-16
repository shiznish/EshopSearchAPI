using Application.Features.Product.Commands;
using Application.Features.Product.Queries;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    // GET: api/<ProductController>
    [HttpGet]
    public async Task<ActionResult<IList<ProductDto>>> Get(string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
    {
        var response = await _mediator.Send(new GetAllProductsQuery(searchTerm, sortColumn, sortOrder, page, pageSize));
        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

    }

    // GET api/<ProductController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);

        Result<ProductDetailsDto> response = await _mediator.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    // POST api/<ProductController>
    [HttpPost]
    public async Task<IActionResult> Post(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);

    }

    // PUT api/<ProductController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
