using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace {{Namespace}}.Api.Controllers;

// Base template for REST controllers
// Replace {{EntityName}} with the domain entity name
// Replace {{RouteName}} with the plural route

[ApiController]
[Route("api/[controller]")]
public class {{EntityName}}Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public {{EntityName}}Controller(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET api/{{RouteName}}
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAll{{EntityName}}Query());
        return Ok(result);
    }

    // GET api/{{RouteName}}/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new Get{{EntityName}}ByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }
}
