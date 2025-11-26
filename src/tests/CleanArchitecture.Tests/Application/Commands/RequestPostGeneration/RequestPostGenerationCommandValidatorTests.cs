using CleanArchitecture.Application.Commands.RequestPostGeneration;
using FluentAssertions;
using CleanArchitecture.Domain.Enums;
using System;
using Xunit;

namespace CleanArchitecture.Tests.Application.Commands.RequestPostGeneration
{
    public class RequestPostGenerationCommandValidatorTests
    {
        private readonly RequestPostGenerationCommandValidator _validator = new();

        [Fact]
        public void Validate_ValidCommand_ShouldPass()
        {
            // Arrange
            var command = new RequestPostGenerationCommand
            {
                TenantId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Topic = "AI",
                Platform = PlatformType.LinkedIn,
                AutoApprove = false
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_EmptyTenantId_ShouldFail()
        {
            // Arrange
            var command = new RequestPostGenerationCommand
            {
                TenantId = Guid.Empty,
                UserId = Guid.NewGuid(),
                Topic = "AI",
                Platform = PlatformType.LinkedIn,
                AutoApprove = false
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_EmptyTopic_ShouldFail()
        {
            // Arrange
            var command = new RequestPostGenerationCommand
            {
                TenantId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Topic = string.Empty,
                Platform = PlatformType.LinkedIn,
                AutoApprove = false
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
        }
    }
}
