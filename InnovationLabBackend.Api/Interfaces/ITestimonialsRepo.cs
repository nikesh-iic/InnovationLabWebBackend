using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Interfaces
{
    public interface ITestimonialsRepo
    {
        Task<List<Testimonial>> GetTestimonialsAsync();
        Task<Testimonial?> GetTestimonialByIdAsync(Guid id);
        Task<Testimonial> CreateTestimonialAsync(Testimonial testimonial);
        Task UpdateTestimonialAsync(Testimonial testimonial);
        Task DeleteTestimonialAsync(Testimonial testimonial);
    }
}
