using InnovationLabBackend.Api.DTO.Testimonials;
using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Mapper
{
    public static class TestimonialsMapper
    {
        public static TestimonialsDTO ToTestimonialsDTO(this Testimonial testimonial)
        {
            return new TestimonialsDTO
            {
                Id = testimonial.Id,
                Name = testimonial.Name,
                Text = testimonial.Text,
                Designation = testimonial.Designation,
                Organization = testimonial.Organization,
                ImageUrl = testimonial.ImageUrl,
                CreatedAt = testimonial.CreatedAt
            };
        }

        public static Testimonial ToTestimonialsFromCreateTestimonials(this CreateTestimonialsDTO dTO)
        {
            return new Testimonial()
            {
                Name = dTO.Name,
                Text = dTO.Text,
                Designation = dTO.Designation,
                Organization = dTO.Organization,
                ImageUrl = dTO.ImageUrl,
                CreatedAt = DateTime.UtcNow
            };
        }
        public static Testimonial ToTestimonialsFromUpdateTestimonials(this UpdateTestimonialsDTO dTO)
        {
            return new Testimonial()
            {
                Name = dTO.Name,
                Text = dTO.Text,
                Designation = dTO.Designation,
                Organization = dTO.Organization,
                ImageUrl = dTO.ImageUrl
            };
        }
    }
}
