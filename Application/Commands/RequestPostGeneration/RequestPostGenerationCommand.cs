using System;
using Domain.Enums;
using MediatR;
using Application.DTOs;

namespace Application.Commands.RequestPostGeneration
{
    public class RequestPostGenerationCommand : IRequest<PostGenerationJobDto>
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public string Topic { get; set; }
        public PlatformType Platform { get; set; }
        public bool AutoApprove { get; set; }
        public string? Tone { get; set; }
        public string? Language { get; set; }
        public string? AdditionalInstructions { get; set; }
    }
}
