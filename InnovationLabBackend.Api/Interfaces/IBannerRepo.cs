using InnovationLabBackend.Api.Dtos.Banner;
using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Interfaces
{
    public interface IBannerRepo
    {
        
        Task<Banner> CreateBannerAsync(Banner banner);
       
    }
}
