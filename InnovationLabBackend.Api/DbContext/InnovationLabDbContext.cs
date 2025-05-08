using InnovationLabBackend.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InnovationLabBackend.Api.DbContext
{
    public class InnovationLabDbContext(DbContextOptions<InnovationLabDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Testimonial> Testimonials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Automatically convert all enums to strings
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.ClrType.GetProperties())
                {
                    if (property.PropertyType.IsEnum)
                    {
                        modelBuilder
                            .Entity(entity.ClrType)
                            .Property(property.Name)
                            .HasConversion<string>();
                    }

                    // Handle nullable enums
                    var underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
                    if (underlyingType != null && underlyingType.IsEnum)
                    {
                        modelBuilder
                            .Entity(entity.ClrType)
                            .Property(property.Name)
                            .HasConversion(typeof(EnumToStringConverter<>).MakeGenericType(underlyingType));
                    }
                }
            }
        }
    }
}