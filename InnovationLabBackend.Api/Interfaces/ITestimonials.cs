using InnovationLabBackend.Api.DTO.Testimonials;
using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Interfaces
{
    public interface ITestimonials
    {
        Task<List<Testimonial>> GetTestimonialsAsync();
        Task<Testimonial> CreateTestimonialAsync(Testimonial testimonial);
        Task<Testimonial?> UpdateTestimonialAsync(string id,UpdateTestimonialsDTO testimonial);
        Task<bool> DeleteTestimonialAsync(string id);
    }
}
