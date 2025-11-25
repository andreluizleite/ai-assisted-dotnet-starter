using System.Threading.Tasks;
using Domain.Aggregates;

namespace Domain.Interfaces
{
    public interface IPostGenerationJobRepository
    {
        Task AddAsync(PostGenerationJob job);
        // Add more methods as needed (e.g., GetByIdAsync)
    }
}
