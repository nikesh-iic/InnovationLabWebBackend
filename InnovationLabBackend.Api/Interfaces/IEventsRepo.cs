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
        Task<EventRegistration> RegisterEventAsync(EventRegistration eventRegistration);
        Task<List<EventRegistration>> GetEventRegistrationsAsync(Guid eventId, EventRegistrationFilterDto filters);
        Task<EventRegistration?> GetEventRegistrationByIdAsync(Guid registrationId);
        Task UpdateEventRegistrationStatusAsync(EventRegistration eventRegistration, EventRegistrationStatus status);
    }
}
