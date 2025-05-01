namespace InnovationLabBackend.Api.Models
{
    public class Testimonial
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string? Designation { get; set; } 
        public string? Organization { get; set; } 
        public string? ImageUrl { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
