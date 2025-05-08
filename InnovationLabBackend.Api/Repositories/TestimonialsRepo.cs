using InnovationLabBackend.Api.DbContext;
using InnovationLabBackend.Api.Interfaces;
using InnovationLabBackend.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InnovationLabBackend.Api.Repository
{
    public class TestimonialsRepo(InnovationLabDbContext dbContext) : ITestimonialsRepo
    {
        private readonly InnovationLabDbContext _dbContext = dbContext;
        
        private async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() != 0;
        }

        public async Task<List<Testimonial>> GetTestimonialsAsync()
        {
            var testimonials = await _dbContext.Testimonials.ToListAsync();
            return testimonials;
        }

        public async Task<Testimonial?> GetTestimonialByIdAsync(Guid id)
        {
            var testimonial = await _dbContext.Testimonials.FirstOrDefaultAsync(t => t.Id == id);
            return testimonial;
        }

        public async Task<Testimonial> CreateTestimonialAsync(Testimonial testimonial)
        {
            await _dbContext.Testimonials.AddAsync(testimonial);
            await SaveChangesAsync();
            return testimonial;
        }

        public async Task UpdateTestimonialAsync(Testimonial testimonial)
        {
            testimonial.UpdatedAt = DateTimeOffset.UtcNow;
            await SaveChangesAsync();
        }

        public async Task DeleteTestimonialAsync(Testimonial testimonial)
        {
            testimonial.IsDeleted = true;
            testimonial.DeletedAt = DateTimeOffset.UtcNow;
            await SaveChangesAsync();
        }
    }
}
