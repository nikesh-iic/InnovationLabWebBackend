using System.ComponentModel.DataAnnotations;
using AutoMapper;
using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Dtos.Events
{
    [AutoMap(typeof(Event), ReverseMap = true)]
    public class CreateEventDto
    {
        public Guid? ParentEventId { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(6)]
        public required IList<string> Highlights { get; set; }
        [Required]
        public required DateTimeOffset StartTime { get; set; }
        [Required]
        public required DateTimeOffset EndTime { get; set; }
        [Required]
        public required string Location { get; set; }
        [Required]
        public required IFormFile CoverImage { get; set; }
        public string? SeriesName { get; set; }
        [Required]
        public required bool IsTeamEvent { get; set; }
        public int? MaxTeamMembers { get; set; }
        public DateTimeOffset? RegistrationStart { get; set; }
        public DateTimeOffset? RegistrationEnd { get; set; }
    }
}