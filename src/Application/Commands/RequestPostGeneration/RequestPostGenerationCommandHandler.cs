using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CleanArchitecture.Domain.Aggregates;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Application.Dtos;

namespace CleanArchitecture.Application.Commands.RequestPostGeneration
{
    public class RequestPostGenerationCommandHandler : IRequestHandler<RequestPostGenerationCommand, PostGenerationJobDto>
    {
        private readonly IPostGenerationJobRepository _repository;

        public RequestPostGenerationCommandHandler(IPostGenerationJobRepository repository)
        {
            _repository = repository;
        }

        public async Task<PostGenerationJobDto> Handle(RequestPostGenerationCommand request, CancellationToken cancellationToken)
        {
            var job = new PostGenerationJob(
                request.TenantId,
                request.UserId,
                request.Topic,
                request.Platform,
                request.AutoApprove
            );

            await _repository.AddAsync(job);

            // Optionally: Append integration event to Outbox here

            return new PostGenerationJobDto
            {
                JobId = job.Id,
                Status = job.Status.ToString(),
                AutoApprove = job.AutoApprove,
                CreatedAt = job.CreatedAt
            };
        }
    }
}
