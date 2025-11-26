using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Worker.Queue;
using CleanArchitecture.Worker.Handlers;

namespace CleanArchitecture.Worker.Configuration
{
    /// <summary>
    /// Configures dependency injection and worker services.
    /// </summary>
    public static class WorkerConfiguration
    {
        public static IServiceCollection AddWorkerServices(this IServiceCollection services)
        {
            services.AddSingleton<IQueueClient, QueueClient>();
            services.AddTransient<PostGenerationJobWorker>();
            // Add other worker-related services/configuration here
            return services;
        }
    }
}
