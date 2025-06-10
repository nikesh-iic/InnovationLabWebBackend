using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InnovationLabBackend.Api.Models
{
    public class Event
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? ParentEventId { get; set; }
        [ForeignKey(nameof(ParentEventId))] public Event? ParentEvent { get; set; }
        [Required] public required string Title { get; set; }
        [Required] public required string Description { get; set; }
        [Required][MinLength(1)][MaxLength(6)] public required IList<string> Highlights { get; set; }
        [Required] public required DateTimeOffset StartTime { get; set; }
        [Required] public required DateTimeOffset EndTime { get; set; }
        [Required] public required string Location { get; set; }
        [Required] public required string CoverImageUrl { get; set; }
        public string? SeriesName { get; set; }
        [Required] public required bool IsTeamEvent { get; set; }
        [Required] public required int MaxTeamMembers { get; set; }
        public DateTimeOffset? RegistrationStart { get; set; }
        public DateTimeOffset? RegistrationEnd { get; set; }
        [Required] public required DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset DeletedAt { get; set; }
    }
}