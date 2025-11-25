using System.Threading.Tasks;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Domain.Aggregates;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class PostGenerationJobRepository : IPostGenerationJobRepository
    {
        private readonly AppDbContext _context;

        public PostGenerationJobRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PostGenerationJob job)
        {
            await _context.PostGenerationJobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        // Add more methods as needed (e.g., GetByIdAsync)
    }
}
