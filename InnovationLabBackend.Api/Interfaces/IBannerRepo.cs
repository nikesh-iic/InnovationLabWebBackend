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
        DateTimeOffset? createdAfter = null);
        Task<BannerGetDTO> GetBannerByIdAsync(Guid id);
        Task DeleteBannerAsync(Guid id);
        Task<Banner> updateBannerAsync(Guid id, BannerUpdateDTO bannerUpdateDTO);
        Task ActivateBanner(Guid id);
        Task<Banner> ScheduleBannerDate(Guid id, DateScheduleDTO dateScheduleDTO);

    }
}
