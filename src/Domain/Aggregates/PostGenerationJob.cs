using System;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Aggregates
{
    /// <summary>
    /// Aggregate root for AI Post Generation Job.
    /// </summary>
    public class PostGenerationJob
    {
        public Guid Id { get; private set; }
        public Guid TenantId { get; private set; }
        public Guid RequestedByUserId { get; private set; }
        public string Topic { get; private set; }
        public PlatformType Platform { get; private set; }
        public bool AutoApprove { get; private set; }
        public PostGenerationJobStatus Status { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        // EF Core constructor
        private PostGenerationJob() { }

        public PostGenerationJob(Guid tenantId, Guid requestedByUserId, string topic, PlatformType platform, bool autoApprove)
        {
            if (tenantId == Guid.Empty)
                throw new ArgumentException("TenantId cannot be empty.", nameof(tenantId));
            if (requestedByUserId == Guid.Empty)
                throw new ArgumentException("RequestedByUserId cannot be empty.", nameof(requestedByUserId));
            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentException("Topic cannot be null or empty.", nameof(topic));

            Id = Guid.NewGuid();
            TenantId = tenantId;
            RequestedByUserId = requestedByUserId;
            Topic = topic;
            Platform = platform;
            AutoApprove = autoApprove;
            Status = PostGenerationJobStatus.Pending;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
