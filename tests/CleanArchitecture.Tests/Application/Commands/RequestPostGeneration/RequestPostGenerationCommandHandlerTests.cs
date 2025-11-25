using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.RequestPostGeneration;
using Application.DTOs;
using Domain.Aggregates;
using Domain.Enums;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanArchitecture.Tests.Application.Commands.RequestPostGeneration
{
    public class RequestPostGenerationCommandHandlerTests
    {
        private readonly Mock<IPostGenerationJobRepository> _repositoryMock;
        private readonly RequestPostGenerationCommandHandler _handler;

        public RequestPostGenerationCommandHandlerTests()
        {
            _repositoryMock = new Mock<IPostGenerationJobRepository>();
            _handler = new RequestPostGenerationCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateJobAndReturnDto()
        {
            // Arrange
            var command = new RequestPostGenerationCommand
            {
                TenantId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Topic = "AI",
                Platform = PlatformType.LinkedIn,
                AutoApprove = true
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.JobId.Should().NotBeEmpty();
            result.Status.Should().Be(PostGenerationJobStatus.Pending.ToString());
            result.AutoApprove.Should().BeTrue();
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<PostGenerationJob>()), Times.Once);
        }
    }
}
