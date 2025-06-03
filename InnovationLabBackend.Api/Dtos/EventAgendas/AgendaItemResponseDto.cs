namespace InnovationLabBackend.Api.Dtos.EventAgendas
{
    public class AgendaItemResponseDto
    {
        public Guid Id { get; set; }
        public required TimeOnly StartTime { get; set; }
        public required TimeOnly EndTime { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}