using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using InnovationLabBackend.Api.Dtos.Banner;
using InnovationLabBackend.Api.Interfaces;
using System.IO;

namespace InnovationLabBackend.Api.Helper
{
    public class UploadMedia : IUploadMedia
    {
        private readonly Cloudinary _cloudinary;

        public UploadMedia(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public  async Task<BannerTypeDTO> UploadMediaAsync(IFormFile file)
        {
            if (file == null)
            {
                return  new BannerTypeDTO { Success = false, ErrorMessage = "File is null." };
            }
            var contentType = file.ContentType.ToLower();
            var isImage = contentType.StartsWith("image");
            var isVideo = contentType.StartsWith("video");

            if (!isImage && !isVideo) {
                return new BannerTypeDTO { Success = false, ErrorMessage = "Unsupported media type." };
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            using var stream = file.OpenReadStream();
            string publicId = $"media/{Guid.NewGuid()}";

            RawUploadResult? result = null;

            if (isImage)
            {
                var imageParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill"),
                    PublicId = publicId
                };

                result = await _cloudinary.UploadAsync(imageParams);
            }

            if (isVideo)
            {
                var videoParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = publicId
                };

                result = await _cloudinary.UploadAsync(videoParams);
            }

            if (result == null || result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new BannerTypeDTO
                {
                    Success = false,
                    ErrorMessage = result?.Error?.Message ?? "Upload failed"
                };
            }

            return new BannerTypeDTO
            {
                Url = result.SecureUrl.ToString(),
                MediaType = isImage ? Enums.BannerType.Image : Enums.BannerType.Video,
            };
        }
    }
}
