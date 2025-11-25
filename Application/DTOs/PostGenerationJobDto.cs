using System;

namespace Application.DTOs
{
    public class PostGenerationJobDto
    {
        public Guid JobId { get; set; }
        public string Status { get; set; }
        public bool AutoApprove { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
