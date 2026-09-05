using CleanArchitecture.Api.Models;
using CleanArchitecture.Application.Commands;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Route("api/v1/customers")]
public sealed class CustomersController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyCollection<CustomerDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<CustomerDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var customers = await sender.Send(new GetAllCustomersQuery(), cancellationToken);
        return Ok(customers);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<CustomerDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var customer = await sender.Send(new GetCustomerByIdQuery(id), cancellationToken);
        return Ok(customer);
    }

    [HttpPost]
    [ProducesResponseType<CustomerDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CustomerDto>> Create(
        CreateCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCustomerCommand(request.FirstName, request.LastName, request.Email);
        var customer = await sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { customer.Id }, customer);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType<CustomerDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CustomerDto>> Update(
        Guid id,
        UpdateCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCustomerCommand(id, request.FirstName, request.LastName, request.Email);
        var customer = await sender.Send(command, cancellationToken);

        return Ok(customer);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteCustomerCommand(id), cancellationToken);
        return NoContent();
    }
}
