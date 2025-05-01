using InnovationLabBackend.Api.DTO.Testimonials;
using InnovationLabBackend.Api.Interfaces;
using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Repository
{
    public class TestimonialsRepo : ITestimonials

    {
        private readonly List<Testimonial> _testimonials = new List<Testimonial>
        {
            new Testimonial
            {
                Id = "1",
                Name = "Ram",
                Text = "This is a great service!",
                Designation = "Software Engineer",
                Organization = "Tech Company",
                ImageUrl = "https://example.com/image.jpg"
            },
            new Testimonial
            {
                Id = "2",
                Name = "Sitaa",
                Text = "I had an amazing experience!",
                Designation = "Product Manager",
                Organization = "Another Company",
                ImageUrl = "https://example.com/image2.jpg"
            }
        };
        public Task<Testimonial> CreateTestimonialAsync(Testimonial testimonial)
        {
            testimonial.Id = Guid.NewGuid().ToString();
            testimonial.CreatedAt = DateTime.UtcNow;
            _testimonials.Add(testimonial);
            return Task.FromResult(testimonial);
        }



        public Task<bool> DeleteTestimonialAsync(string id)
        {
            var newId = _testimonials.FirstOrDefault(x=>x.Id == id);
            if (newId != null)
            {
                _testimonials.Remove(newId);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public  Task<List<Testimonial>> GetTestimonialsAsync()
        {
            return Task.FromResult(_testimonials.ToList());
        }

        

public Task<Testimonial?> UpdateTestimonialAsync(string id, UpdateTestimonialsDTO testimonial)
        {
            var existingTestimonial = _testimonials.FirstOrDefault(x => x.Id == id);
            if (existingTestimonial != null)
            {
                existingTestimonial.Name = testimonial.Name ?? existingTestimonial.Name;
                existingTestimonial.Text = testimonial.Text ?? existingTestimonial.Text;
                existingTestimonial.Designation = testimonial.Designation ?? existingTestimonial.Designation;
                existingTestimonial.Organization = testimonial.Organization ?? existingTestimonial.Organization;
                existingTestimonial.ImageUrl = testimonial.ImageUrl ?? existingTestimonial.ImageUrl;
                return Task.FromResult<Testimonial?>(existingTestimonial); 
            }
            return Task.FromResult<Testimonial?>(null);
        }
    }
}
