using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InnovationLabBackend.Api.Enums;

namespace InnovationLabBackend.Api.Models
{
    public class EventRegistration
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public required Guid EventId { get; set; }
        [ForeignKey(nameof(EventId))]
        public required Event Event { get; set; }
        [Required]
        public required EventRegistrationType Type { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [Phone]
        public required string Phone { get; set; }
        [Required]
        public required EventRegistrationStatus Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; }
    }
}