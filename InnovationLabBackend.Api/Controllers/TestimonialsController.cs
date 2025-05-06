using InnovationLabBackend.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InnovationLabBackend.Api.Dtos.Testimonials;
using AutoMapper;
using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Controllers
{
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class TestimonialsController(ITestimonialsRepo testimonialsRepo, IMapper mapper) : ControllerBase
    {
        private readonly ITestimonialsRepo _testimonialsRepo = testimonialsRepo;
        private readonly IMapper _mapper = mapper;

        [HttpGet(Name = "GetTestimonials")]
        public async Task<ActionResult<List<TestimonialResponseDto>>> GetTestimonials()
        {
            var testimonials = await _testimonialsRepo.GetTestimonialsAsync();
            var testimonialsDto = _mapper.Map<List<TestimonialResponseDto>>(testimonials);
            return Ok(testimonialsDto);
        }

        [HttpGet("{id}", Name = "GetTestimonialById")]
        public async Task<ActionResult<TestimonialResponseDto>> GetTestimonialById(Guid id)
        {
            var testimonial = await _testimonialsRepo.GetTestimonialByIdAsync(id);

            if (testimonial == null)
            {
                return NotFound(new
                {
                    Message = "Testinomial Not Found"
                });
            }

            var testimonialDto = _mapper.Map<TestimonialResponseDto>(testimonial);
            return Ok(testimonialDto);
        }

        [Authorize]
        [HttpPost(Name = "CreateTestimonial")]
        public async Task<ActionResult<TestimonialResponseDto>> CreateTestimonial([FromBody] CreateTestimonialDto testimonialCreateDto)
        {
            var newTestimonial = _mapper.Map<Testimonial>(testimonialCreateDto);
            var createdTestimonial = await _testimonialsRepo.CreateTestimonialAsync(newTestimonial);

            return CreatedAtAction(nameof(GetTestimonials), new { id = createdTestimonial.Id }, createdTestimonial);
        }

        [HttpPatch("{id}", Name = "UpdateTestimonial")]
        public async Task<ActionResult> UpdateTestimonial(Guid id, [FromBody] UpdateTestimonialDto testimonialUpdateDto)
        {
            if (testimonialUpdateDto == null)
            {
                return BadRequest("Invalid data");
            }

            var testimonial = await _testimonialsRepo.GetTestimonialByIdAsync(id);

            if (testimonial == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(testimonialUpdateDto, testimonial);
            await _testimonialsRepo.UpdateTestimonialAsync(testimonial);

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteTestimonial")]
        public async Task<ActionResult> DeleteTestimonial(Guid id)
        {
            var testimonial = await _testimonialsRepo.GetTestimonialByIdAsync(id);

            if (testimonial == null)
            {
                return NotFound();
            }

            await _testimonialsRepo.DeleteTestimonialAsync(testimonial);

            return NoContent();
        }

    }
}
