using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InnovationLabBackend.Api.Controllers
{
    public class UploadImageController : ControllerBase 
    {
        public async Task<IActionResult?> UploadImage(IFormFile file)
        {

            var cloudinary = new Cloudinary();
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Width(500).Height(500).Crop("fill"),
                PublicId = $"testimonials/{Guid.NewGuid()}"
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            if (uploadResult.Error != null)
            {
                return BadRequest(new { Message = "Image upload failed", Error = uploadResult.Error.Message });
            }
           
            var fileUrl = uploadResult.SecureUrl.ToString();
            return Ok(new { fileUrl });
        }
    }
}
