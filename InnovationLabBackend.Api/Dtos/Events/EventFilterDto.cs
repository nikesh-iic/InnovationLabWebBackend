using InnovationLabBackend.Api.Enums;

namespace InnovationLabBackend.Api.Dtos.Events
{
    public class EventFilterDto
    {
        public EventStatus? Status { get; set; }
        public Guid? ParentEventId { get; set; }
        public string? SeriesName { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public EventSortBy SortBy { get; set; } = EventSortBy.StartTime;
        public SortOrder SortOrder { get; set; } = SortOrder.Asc;
    }
}