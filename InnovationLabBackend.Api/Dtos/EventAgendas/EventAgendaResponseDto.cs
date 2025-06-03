namespace InnovationLabBackend.Api.Dtos.EventAgendas
{
    public class EventAgendaResponseDto
    {
        public Guid Id { get; set; }
        public required Guid EventId { get; set; }
        public required int Day { get; set; }
        public required List<AgendaItemResponseDto> Items { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset DeletedAt { get; set; }
    }
}