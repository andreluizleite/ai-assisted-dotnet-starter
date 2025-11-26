using MediatR;

namespace CleanArchitecture.Application.Commands.PostGeneration;

/// <summary>
/// Command to process a PostGenerationJob by JobId.
/// </summary>
public class ProcessPostGenerationJobCommand : IRequest
{
    public Guid JobId { get; }

    public ProcessPostGenerationJobCommand(Guid jobId)
    {
        JobId = jobId;
    }
}
