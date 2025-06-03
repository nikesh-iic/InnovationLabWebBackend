using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InnovationLabBackend.Api.Models
{
    public class AgendaItem
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        [Required] public required Guid AgendaId { get; set; }
        [ForeignKey(nameof(AgendaId))] public EventAgenda? Agenda { get; set; }
        [Required] public required TimeOnly StartTime { get; set; }
        [Required] public required TimeOnly EndTime { get; set; }
        [Required] public required string Title { get; set; }
        [Required] public required string Description { get; set; }
    }
}