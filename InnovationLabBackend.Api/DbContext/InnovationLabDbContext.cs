using InnovationLabBackend.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InnovationLabBackend.Api.DbContext
{
    public class InnovationLabDbContext(DbContextOptions<InnovationLabDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Testimonial> Testimonials { get; set; }
    }
}