using Xunit;
using FluentAssertions;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using System;

namespace CleanArchitecture.Tests.Domain.Entities
{
    public class CustomerTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "John";
            var lastName = "Doe";
            var email = new Email("john.doe@example.com");

            // Act
            var customer = new Customer(id, firstName, lastName, email);

            // Assert
            customer.Id.Should().Be(id);
            customer.FirstName.Should().Be(firstName);
            customer.LastName.Should().Be(lastName);
            customer.Email.Should().Be(email);
            customer.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            customer.UpdatedAt.Should().BeNull();
        }

        [Fact]
        public void Update_ShouldChangePropertiesAndSetUpdatedAt()
        {
            // Arrange
            var id = Guid.NewGuid();
            var customer = new Customer(id, "John", "Doe", new Email("john.doe@example.com"));
            var newFirstName = "Jane";
            var newLastName = "Smith";
            var newEmail = new Email("jane.smith@example.com");

            // Act
            customer.Update(newFirstName, newLastName, newEmail);

            // Assert
            customer.FirstName.Should().Be(newFirstName);
            customer.LastName.Should().Be(newLastName);
            customer.Email.Should().Be(newEmail);
            customer.UpdatedAt.Should().NotBeNull();
        }
    }
}
