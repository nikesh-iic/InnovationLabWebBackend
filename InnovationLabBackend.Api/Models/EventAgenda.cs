using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InnovationLabBackend.Api.Models
{
    public class EventAgenda
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        [Required] public required Guid EventId { get; set; }
        [ForeignKey(nameof(EventId))] public Event? Event { get; set; }
        [Required] public required int Day { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset DeletedAt { get; set; }
    }
}