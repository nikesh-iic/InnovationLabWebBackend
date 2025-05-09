using InnovationLabBackend.Api.Dtos.Banner;

namespace InnovationLabBackend.Api.Interfaces
{
    public interface IUploadMedia
    {
        Task<BannerTypeDTO> UploadMediaAsync(IFormFile file);
    }
}
