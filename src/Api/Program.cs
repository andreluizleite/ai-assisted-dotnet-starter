using CleanArchitecture.Api.Errors;
using CleanArchitecture.Application;
using CleanArchitecture.Application.Commands;
using CleanArchitecture.Application.Common.Behaviors;
using CleanArchitecture.Application.Validators;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=architecture.db";

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(connectionString));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddHealthChecks().AddDbContextCheck<AppDbContext>("database");

builder.Services.AddMediatR(
    configuration => configuration.RegisterServicesFromAssembly(ApplicationAssembly.Reference));
builder.Services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerValidator>();
builder.Services.AddScoped<IValidator<UpdateCustomerCommand>, UpdateCustomerValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();

app.UseExceptionHandler();
app.MapOpenApi();
app.MapHealthChecks("/health");
app.MapControllers();
app.MapGet(
    "/",
    () => Results.Ok(
        new
        {
            name = "AI-Assisted .NET Architecture Starter",
            api = "/api/v1/customers",
            health = "/health",
            openApi = "/openapi/v1.json"
        }));

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DatabaseInitializer.InitializeAsync(dbContext);
}

await app.RunAsync();

public partial class Program;
