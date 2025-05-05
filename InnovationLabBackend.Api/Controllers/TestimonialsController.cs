using InnovationLabBackend.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using InnovationLabBackend.Api.Mapper;
using InnovationLabBackend.Api.DTO.Testimonials;
using Microsoft.AspNetCore.Authorization;

namespace InnovationLabBackend.Api.Controllers
{
    [Route("testimonials")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonials _testimonials;
        public TestimonialsController(ITestimonials testimonials)
        {
            _testimonials = testimonials;
        }

        [HttpGet]
        public async Task<IActionResult> GetTestimonials()
        {
            var testimonials = await _testimonials.GetTestimonialsAsync();
            var testimonialsDTO = testimonials.Select(s=> s.ToTestimonialsDTO());
            return Ok(testimonialsDTO);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTestimonial([FromBody] CreateTestimonialsDTO testimonial)
        {
            var newTestimonial = testimonial.ToTestimonialsFromCreateTestimonials();
            var createdTestimonial = await _testimonials.CreateTestimonialAsync(newTestimonial);

            return CreatedAtAction(nameof(GetTestimonials), new { id = createdTestimonial.Id }, createdTestimonial);
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTestimonial([FromRoute]string id, [FromBody] UpdateTestimonialsDTO testimonial)
        {
            if (testimonial == null)
            {
                return BadRequest("Invalid data.");
            }
            var updatedTestimonial = await _testimonials.UpdateTestimonialAsync(id, testimonial);
            if (updatedTestimonial != null)
            {
                return Ok(updatedTestimonial);
            }
            return NotFound("Testimonial not found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestimonial([FromRoute]string id)
        {
            if (await _testimonials.DeleteTestimonialAsync(id))
            {
                return NoContent();
            }
            return NotFound("Testimonial not found");
        }

    }
}
