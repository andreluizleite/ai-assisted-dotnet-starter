using System.Threading.Tasks;
using CleanArchitecture.Domain.Aggregates;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IPostGenerationJobRepository
    {
        Task AddAsync(PostGenerationJob job);
        // Add more methods as needed (e.g., GetByIdAsync)
    }
}
