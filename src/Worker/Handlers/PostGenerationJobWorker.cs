using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Worker.Handlers
{
    /// <summary>
    /// Worker responsible for processing PostGenerationJob messages from the queue.
    /// </summary>
    public class PostGenerationJobWorker
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PostGenerationJobWorker> _logger;

        public PostGenerationJobWorker(IMediator mediator, ILogger<PostGenerationJobWorker> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Processes a PostGenerationJob message by sending the command to the Application layer.
        /// </summary>
        /// <param name="jobId">The JobId to process.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task ProcessAsync(Guid jobId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Processing PostGenerationJob with JobId: {JobId}", jobId);
                var command = new Application.Commands.PostGeneration.ProcessPostGenerationJobCommand(jobId);
                await _mediator.Send(command, cancellationToken);
                _logger.LogInformation("Successfully processed PostGenerationJob with JobId: {JobId}", jobId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process PostGenerationJob with JobId: {JobId}", jobId);
                // Handle retry logic or dead-lettering as appropriate
                throw;
            }
        }
    }
}

