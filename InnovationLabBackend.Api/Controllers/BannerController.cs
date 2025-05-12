using AutoMapper;
using InnovationLabBackend.Api.Dtos.Banner;
using InnovationLabBackend.Api.Dtos.Banners;
using InnovationLabBackend.Api.Helper;
using InnovationLabBackend.Api.Interfaces;
using InnovationLabBackend.Api.Models;
using InnovationLabBackend.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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

        [HttpGet(Name = "GetAllBanners")]
        public async Task<ActionResult<IEnumerable<BannerGetDTO>>> GetAllBanners(
            [FromQuery] Enums.BannerType? type = null,
            [FromQuery] DateTimeOffset? startDate = null,
            [FromQuery] DateTimeOffset? endDate = null,
            [FromQuery] DateTimeOffset? createdAfter = null)
        {
            var result = await bannerRepo.GetAllBannerAsync(type, startDate, endDate, createdAfter);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BannerGetDTO>> GetBannerById(Guid id)
        {
            var result = await bannerRepo.GetBannerByIdAsync(id);
            if (result == null)
                return NotFound(new { message = $"Banner with ID  not found." });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBannerById(Guid id)
        {
            await bannerRepo.DeleteBannerAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateBanner([FromRoute] Guid id, [FromForm] BannerUpdateDTO banner)
        {
            if (banner == null)
            {
                return BadRequest();
            }
            var updatedBanner = await bannerRepo.updateBannerAsync(id, banner);
            if (updatedBanner == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateBanner([FromRoute] Guid id)
        {
            try
            {
                await bannerRepo.ActivateBanner(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound($"Banner with id {id} not found." + ex);
            }
        }

        [HttpPut("{id}/schedule")]
        public async Task<ActionResult> ScheduleDateBanner([FromRoute]Guid id, [FromBody] DateScheduleDTO dateScheduleDTO)
        {
            try
            {
                await bannerRepo.ScheduleBannerDate(id, dateScheduleDTO);
                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound($"Banner with id {id} not found." + ex);
            }
        }
    }
}
