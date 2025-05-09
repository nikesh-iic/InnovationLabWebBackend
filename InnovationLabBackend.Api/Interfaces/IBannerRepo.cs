using InnovationLabBackend.Api.Dtos.Banner;
using InnovationLabBackend.Api.Dtos.Banners;
using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Interfaces
{
    public interface IBannerRepo
    {
        
        Task<Banner> CreateBannerAsync(Banner banner);
        Task<IEnumerable<BannerGetDTO>> GetAllBannerAsync(Enums.BannerType? type = null,
        DateTimeOffset? startDate = null,
        DateTimeOffset? endDate = null,
        DateTimeOffset? createdAfter= null);

        Task<BannerGetDTO> GetBannerByIdAsync(Guid id);
       
    }
}
