using InnovationLabBackend.Api.Enums;

namespace InnovationLabBackend.Api.Dtos.Events
{
    public class EventRegistrationFilterDto
    {
        public EventRegistrationStatus? Status { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}