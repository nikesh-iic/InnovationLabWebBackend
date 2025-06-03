using AutoMapper;
using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Dtos.Events
{
    [AutoMap(typeof(Event), ReverseMap = true)]
    public class EventResponseDto
    {
        public Guid Id { get; set; }
        public Guid? ParentEventId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required IList<string> Highlights { get; set; }
        public required DateTimeOffset StartTime { get; set; }
        public required DateTimeOffset EndTime { get; set; }
        public required string Location { get; set; }
        public required string CoverImageUrl { get; set; }
        public string? SeriesName { get; set; }
        public required bool IsTeamEvent { get; set; }
        public required int MaxTeamMembers { get; set; }
        public DateTimeOffset? RegistrationStart { get; set; }
        public DateTimeOffset? RegistrationEnd { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public required DateTimeOffset UpdatedAt { get; set; }
    }
}