using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Interfaces
{
    public interface ICategoriesRepo
    {
        Task<bool> SaveChangesAsync();

        Task<List<Category>> GetAllCategoriesAsync();
        Task<List<Category>> GetTopLevelCategoriesAsync();
        Task<List<Category>> SearchCategoriesByNameAsync(string keyword);
        Task<Category?> GetCategoryByIdAsync(Guid id);

        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(Category category);
    }
}
