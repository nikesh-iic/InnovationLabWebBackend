using InnovationLabBackend.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InnovationLabBackend.Api.DbContext
{
    public class InnovationLabDbContext(DbContextOptions<InnovationLabDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<CoreValue> CoreValues { get; set; }
        public DbSet<JourneyItem> JourneyItems { get; set; }
    }
}