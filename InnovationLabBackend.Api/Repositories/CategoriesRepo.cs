using InnovationLabBackend.Api.DbContext;
using InnovationLabBackend.Api.Interfaces;
using InnovationLabBackend.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InnovationLabBackend.Api.Repositories
{
    public class CategoriesRepo(InnovationLabDbContext _dbContext) : ICategoriesRepo
    {
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories
                .Include(c => c.Subcategories)
                .ToListAsync();
        }

        public async Task<List<Category>> GetTopLevelCategoriesAsync()
        {
            return await _dbContext.Categories
                .Where(c => c.ParentCategoryId == null)
                .Include(c => c.Subcategories)
                .ToListAsync();
        }

        public async Task<List<Category>> SearchCategoriesByNameAsync(string keyword)
        {
            return await _dbContext.Categories
                .Where(c => c.Name.Contains(keyword))
                .Include(c => c.Subcategories)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            return await _dbContext.Categories
                .Include(c => c.Subcategories)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            category.CreatedAt = DateTimeOffset.UtcNow;
            category.LastUpdatedAt = DateTimeOffset.UtcNow;

            var entry = await _dbContext.Categories.AddAsync(category);
            await SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            category.LastUpdatedAt = DateTimeOffset.UtcNow;
            _dbContext.Categories.Update(category);
            await SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(Category category)
        {
            _dbContext.Categories.Remove(category);
            return await SaveChangesAsync();
        }
    }
}
