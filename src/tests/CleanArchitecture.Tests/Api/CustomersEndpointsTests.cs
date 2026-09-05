using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CleanArchitecture.Application.Dtos;

namespace CleanArchitecture.Tests.Api;

public sealed class CustomersEndpointsTests
{
    [Fact]
    public async Task Health_ReturnsOk()
    {
        using var factory = new ArchitectureApiFactory();
        using var client = factory.CreateClient();

        using var response = await client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ReturnsSeedCustomers()
    {
        using var factory = new ArchitectureApiFactory();
        using var client = factory.CreateClient();

        var customers = await client.GetFromJsonAsync<CustomerDto[]>("/api/v1/customers");

        Assert.NotNull(customers);
        Assert.Equal(2, customers.Length);
        Assert.Contains(customers, customer => customer.Email == "ada@example.com");
    }

    [Fact]
    public async Task Create_PersistsCustomerAndReturnsCreated()
    {
        using var factory = new ArchitectureApiFactory();
        using var client = factory.CreateClient();

        using var createResponse = await client.PostAsJsonAsync(
            "/api/v1/customers",
            new { firstName = "Grace", lastName = "Hopper", email = "grace@example.com" });

        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var created = await createResponse.Content.ReadFromJsonAsync<CustomerDto>();
        Assert.NotNull(created);

        var loaded = await client.GetFromJsonAsync<CustomerDto>(
            $"/api/v1/customers/{created.Id}");
        Assert.Equal("grace@example.com", loaded?.Email);
    }

    [Fact]
    public async Task Create_WithInvalidInput_ReturnsValidationProblem()
    {
        using var factory = new ArchitectureApiFactory();
        using var client = factory.CreateClient();

        using var response = await client.PostAsJsonAsync(
            "/api/v1/customers",
            new { firstName = "", lastName = "Hopper", email = "invalid" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var json = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(json);
        Assert.Equal("Validation failed", document.RootElement.GetProperty("title").GetString());
        Assert.True(document.RootElement.TryGetProperty("errors", out _));
    }

    [Fact]
    public async Task Create_WithExistingEmail_ReturnsConflict()
    {
        using var factory = new ArchitectureApiFactory();
        using var client = factory.CreateClient();

        using var response = await client.PostAsJsonAsync(
            "/api/v1/customers",
            new { firstName = "Another", lastName = "Ada", email = "ADA@example.com" });

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task GetById_WhenCustomerDoesNotExist_ReturnsNotFound()
    {
        using var factory = new ArchitectureApiFactory();
        using var client = factory.CreateClient();

        using var response = await client.GetAsync($"/api/v1/customers/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAndDelete_CompleteCustomerLifecycle()
    {
        using var factory = new ArchitectureApiFactory();
        using var client = factory.CreateClient();
        var customers = await client.GetFromJsonAsync<CustomerDto[]>("/api/v1/customers");
        var customer = Assert.Single(customers!, value => value.Email == "ada@example.com");

        using var updateResponse = await client.PutAsJsonAsync(
            $"/api/v1/customers/{customer.Id}",
            new { firstName = "Augusta Ada", lastName = "Lovelace", email = "augusta@example.com" });

        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
        var updated = await updateResponse.Content.ReadFromJsonAsync<CustomerDto>();
        Assert.Equal("Augusta Ada", updated?.FirstName);

        using var deleteResponse = await client.DeleteAsync($"/api/v1/customers/{customer.Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        using var getResponse = await client.GetAsync($"/api/v1/customers/{customer.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}
