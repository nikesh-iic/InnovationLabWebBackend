namespace InnovationLabBackend.Api.DTO.Testimonials
{
    public class TestimonialsDTO
    {
        public string Id { get; set; }
        public string Name { get; set; } 
        public string Text { get; set; }
        public string Designation { get; set; } = string.Empty;
        public string Organization { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
