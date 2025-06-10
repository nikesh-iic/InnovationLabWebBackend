using System.Linq.Expressions;
using InnovationLabBackend.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InnovationLabBackend.Api.DbContext
{
    public class InnovationLabDbContext(DbContextOptions<InnovationLabDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventAgenda> EventAgendas { get; set; }
        public DbSet<AgendaItem> AgendaItems { get; set; }
        public DbSet<EventRegistration> EventRegistrations { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<CoreValue> CoreValues { get; set; }
        public DbSet<JourneyItem> JourneyItems { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            // Enforcing hard delete for both EventAgenda and AgendaItem data using Cascade behaviour
            modelBuilder.Entity<EventAgenda>()
                .HasMany(a => a.Items)
                .WithOne(i => i.Agenda)
                .HasForeignKey(i => i.AgendaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Global query filter for soft delete
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                var isDeletedProp = clrType.GetProperty("IsDeleted");
                if (isDeletedProp != null && isDeletedProp.PropertyType == typeof(bool))
                {
                    // Build the lambda: (e) => !e.IsDeleted
                    var parameter = Expression.Parameter(clrType, "e");
                    var prop = Expression.Property(parameter, isDeletedProp);
                    var filter = Expression.Lambda(
                        Expression.Equal(prop, Expression.Constant(false)),
                        parameter
                    );
                    modelBuilder.Entity(clrType).HasQueryFilter(filter);
                }
            }

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