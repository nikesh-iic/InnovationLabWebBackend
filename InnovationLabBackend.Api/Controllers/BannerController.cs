using AutoMapper;
using InnovationLabBackend.Api.Dtos.Banner;
using InnovationLabBackend.Api.Helper;
using InnovationLabBackend.Api.Interfaces;
using InnovationLabBackend.Api.Models;
using InnovationLabBackend.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InnovationLabBackend.Api.Controllers
{
    [ApiController]
  
    [Route("api/v1/[controller]")]
    public class BannerController : ControllerBase
    {
        private readonly IBannerRepo bannerRepo;
        private readonly IUploadMedia uploadMediaHelper;
        private readonly IMapper mapper; 

        public BannerController(IBannerRepo bannerRepo, IUploadMedia uploadMediaHelper, IMapper mapper)
        {
            this.bannerRepo = bannerRepo;
            this.uploadMediaHelper = uploadMediaHelper;
            this.mapper = mapper; 
        }

        [HttpPost(Name = "CreateBanner")]
        public async Task<ActionResult<BannerDTO>> CreateBanner([FromForm] CreateBannerDTO bannerDto)
        {
            if (bannerDto == null)
            {
                return BadRequest("Banner data is required.");
            }
            var uploadMedaTypeResponse = await uploadMediaHelper.UploadMediaAsync(bannerDto.Url);
            if (uploadMedaTypeResponse == null || uploadMedaTypeResponse.Url == null)
            {
                return BadRequest("Media upload failed.");
            }

            var newBanner = mapper.Map<Banner>(bannerDto);
            newBanner.Url = uploadMedaTypeResponse.Url; 

            var createdBanner = await bannerRepo.CreateBannerAsync(newBanner);
            return CreatedAtAction(nameof(CreateBanner), new { id = createdBanner.Id }, createdBanner);
        }
    }
}
