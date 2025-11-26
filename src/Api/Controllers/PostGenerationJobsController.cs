using System.Threading.Tasks;
using CleanArchitecture.Application.Commands.RequestPostGeneration;
using CleanArchitecture.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostGenerationJobsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostGenerationJobsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Requests the creation of a new AI-generated post job.
        /// </summary>
        /// <param name="command">The request details.</param>
        /// <returns>The created job info.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PostGenerationJobDto), 200)]
        public async Task<IActionResult> RequestPostGeneration([FromBody] RequestPostGenerationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}

