using CloudinaryDotNet;
using InnovationLabBackend.Api.Enums;

namespace InnovationLabBackend.Api.Interfaces
{
    public interface IMediaService
    {
        Task<string?> UploadAsync(IFormFile file, MediaType mediaType, string? folder = null, Transformation? transformation = null);
    }
}