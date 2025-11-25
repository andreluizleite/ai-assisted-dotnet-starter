using System;

namespace CleanArchitecture.Application.Dtos
{
    public class PostGenerationJobDto
    {
        public Guid JobId { get; set; }
        public string Status { get; set; }
        public bool AutoApprove { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
