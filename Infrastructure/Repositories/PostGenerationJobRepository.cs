using System.Threading.Tasks;
using Domain.Aggregates;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
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
