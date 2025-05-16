using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Interfaces
{
    public interface IFaqsRepo
    {
        Task<bool> SaveChangesAsync();
        Task<List<Faq>> GetFaqsAsync(string? category, string sortBy, string sortOrder, int page, int limit);
        Task<Faq?> GetFaqByIdAsync(Guid id);
        Task<Faq> CreateFaqAsync(Faq faq);
        Task<Faq> UpdateFaqAsync(Faq faq);
        Task<bool> DeleteFaqAsync(Faq faq);
        Task<int> GetTotalCountAsync(string? category);
    }
}
