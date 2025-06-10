using InnovationLabBackend.Api.Dtos.EventRegistrations;
using InnovationLabBackend.Api.Dtos.Events;
using InnovationLabBackend.Api.Enums;
using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Interfaces
{
    public interface IEventsRepo
    {
        Task<List<Event>> GetEventsAsync(EventFilterDto filters);
        Task<Event?> GetEventByIdAsync(Guid id);
        Task<Event> CreateEventAsync(Event ev);
        Task UpdateEventAsync(Event ev);
        Task DeleteEventAsync(Event ev);
        Task<List<EventAgenda>> GetEventAgendaAsync(Guid eventId);
        Task<EventAgenda?> GetEventAgendaByIdAsync(Guid agendaId);
        Task<EventAgenda> CreateEventAgendaAsync(EventAgenda eventAgenda);
        Task UpdateEventAgendaAsync(EventAgenda eventAgenda);
        Task DeleteEventAgendaAsync(EventAgenda eventAgenda);
        Task<EventRegistration> RegisterEventAsync(EventRegistration eventRegistration);
        Task<List<EventRegistration>> GetEventRegistrationsAsync(Guid eventId, EventRegistrationFilterDto filters);
        Task<EventRegistration?> GetEventRegistrationByIdAsync(Guid registrationId);
        Task UpdateEventRegistrationStatusAsync(EventRegistration eventRegistration, EventRegistrationStatus status);
    }
}
