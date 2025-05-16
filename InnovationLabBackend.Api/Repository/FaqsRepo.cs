using InnovationLabBackend.Api.DbContext;
using InnovationLabBackend.Api.Interfaces;
using InnovationLabBackend.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InnovationLabBackend.Api.Repository
{
    public class FaqsRepo : IFaqsRepo
    {
        private readonly InnovationLabDbContext _dbContext;

        public FaqsRepo(InnovationLabDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Faq>> GetFaqsAsync(string? category, string sortBy, string sortOrder, int page, int limit)
        {
            var query = _dbContext.Faqs.Include(f => f.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(f => f.Category.Name.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            query = sortBy switch
            {
                "question" => sortOrder == "asc" ? query.OrderBy(f => f.Question) : query.OrderByDescending(f => f.Question),
                _ => sortOrder == "asc" ? query.OrderBy(f => f.CreatedAt) : query.OrderByDescending(f => f.CreatedAt),
            };

            return await query.Skip((page - 1) * limit).Take(limit).ToListAsync();
        }

        public async Task<Faq?> GetFaqByIdAsync(Guid id)
        {
            return await _dbContext.Faqs
                .Include(f => f.Category)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Faq> CreateFaqAsync(Faq faq)
        {
            faq.CreatedAt = DateTimeOffset.UtcNow;
            faq.LastUpdatedAt = DateTimeOffset.UtcNow;

            var entry = await _dbContext.Faqs.AddAsync(faq);
            await SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<Faq> UpdateFaqAsync(Faq faq)
        {
            faq.LastUpdatedAt = DateTimeOffset.UtcNow;

            _dbContext.Faqs.Update(faq);
            await SaveChangesAsync();

            return faq;
        }

        public async Task<bool> DeleteFaqAsync(Faq faq)
        {
            _dbContext.Faqs.Remove(faq);
            return await SaveChangesAsync();
        }

        public async Task<int> GetTotalCountAsync(string? category)
        {
            var query = _dbContext.Faqs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(f => f.Category.Name.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            return await query.CountAsync();
        }

    }
}
