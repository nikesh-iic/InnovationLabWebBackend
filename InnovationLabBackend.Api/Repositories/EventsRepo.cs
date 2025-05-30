using InnovationLabBackend.Api.DbContext;
using InnovationLabBackend.Api.Dtos.Events;
using InnovationLabBackend.Api.Enums;
using InnovationLabBackend.Api.Interfaces;
using InnovationLabBackend.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InnovationLabBackend.Api.Repositories
{
    public class EventsRepo(InnovationLabDbContext dbContext) : IEventsRepo
    {
        private readonly InnovationLabDbContext _dbContext = dbContext;

        private async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() != 0;
        }

        public async Task<List<Event>> GetEventsAsync(EventFilterDto filters)
        {
            var query = _dbContext.Events.AsQueryable();

            Console.WriteLine("****************************");
            Console.WriteLine(filters.Status);
            Console.WriteLine("****************************");

            // Filter by Status (Ongoing, Upcoming, Past)
            if (filters.Status.HasValue)
            {
                var now = DateTimeOffset.UtcNow;
                query = filters.Status.Value switch
                {
                    EventStatus.Ongoing => query.Where(e => e.StartTime <= now && e.EndTime >= now),
                    EventStatus.Upcoming => query.Where(e => e.StartTime > now),
                    EventStatus.Past => query.Where(e => e.EndTime < now),
                    _ => query
                };
            }

            // Filter by ParentEventId
            if (filters.ParentEventId.HasValue)
            {
                query = query.Where(e => e.ParentEventId == filters.ParentEventId.Value);
            }

            // Filter by SeriesName
            if (!string.IsNullOrWhiteSpace(filters.SeriesName))
            {
                query = query.Where(e => e.SeriesName != null && e.SeriesName.ToLower().Contains(filters.SeriesName.ToLower()));
            }

            // Sorting 
            var sortOrder = filters.SortOrder == SortOrder.Desc ? "desc" : "asc";
            query = (filters.SortBy, sortOrder) switch
            {
                (EventSortBy.Title, "asc") => query.OrderBy(e => e.Title),
                (EventSortBy.Title, "desc") => query.OrderByDescending(e => e.Title),
                (EventSortBy.StartTime, "asc") => query.OrderBy(e => e.StartTime),
                (EventSortBy.StartTime, "desc") => query.OrderByDescending(e => e.StartTime),
                (EventSortBy.EndTime, "asc") => query.OrderBy(e => e.EndTime),
                (EventSortBy.EndTime, "desc") => query.OrderByDescending(e => e.EndTime),
                (EventSortBy.RegistrationStart, "asc") => query.OrderBy(e => e.RegistrationStart),
                (EventSortBy.RegistrationStart, "desc") => query.OrderByDescending(e => e.RegistrationStart),
                (EventSortBy.RegistrationEnd, "asc") => query.OrderBy(e => e.RegistrationEnd),
                (EventSortBy.RegistrationEnd, "desc") => query.OrderByDescending(e => e.RegistrationEnd),
                _ => query.OrderBy(e => e.StartTime)
            };

            // Paging
            var page = filters.Page < 1 ? 1 : filters.Page;
            var limit = filters.Limit < 1 ? 10 : filters.Limit;
            query = query.Skip((page - 1) * limit).Take(limit);

            var events = await query.ToListAsync();
            return events;
        }

        public async Task<Event?> GetEventByIdAsync(Guid id)
        {
            var ev = await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);
            return ev;
        }

        public async Task<Event> CreateEventAsync(Event ev)
        {
            await _dbContext.Events.AddAsync(ev);
            await SaveChangesAsync();
            return ev;
        }

        public async Task UpdateEventAsync(Event ev)
        {
            ev.UpdatedAt = DateTimeOffset.UtcNow;
            await SaveChangesAsync();
        }

        public async Task DeleteEventAsync(Event ev)
        {
            ev.IsDeleted = true;
            ev.DeletedAt = DateTimeOffset.UtcNow;
            await SaveChangesAsync();
        }

        public async Task<EventRegistration> RegisterEventAsync(EventRegistration eventRegistration)
        {
            await _dbContext.EventRegistrations.AddAsync(eventRegistration);
            await SaveChangesAsync();
            return eventRegistration;
        }

        public async Task<List<EventRegistration>> GetEventRegistrationsAsync(Guid eventId, EventRegistrationFilterDto filters)
        {
            var query = _dbContext.EventRegistrations
                .Where(er => er.EventId == eventId);

            // Filter by Status
            if (filters.Status.HasValue)
            {
                query = query.Where(er => er.Status == filters.Status.Value);
            }

            // Paging
            var page = filters.Page < 1 ? 1 : filters.Page;
            var limit = filters.Limit < 1 ? 10 : filters.Limit;
            query = query.Skip((page - 1) * limit).Take(limit);

            var registrations = await query.ToListAsync();
            return registrations;
        }

        public async Task<EventRegistration?> GetEventRegistrationByIdAsync(Guid registrationId)
        {
            var registration = await _dbContext.EventRegistrations.FirstOrDefaultAsync(er => er.Id == registrationId);
            return registration;
        }

        public async Task UpdateEventRegistrationStatusAsync(EventRegistration eventRegistration, EventRegistrationStatus status)
        {
            eventRegistration.UpdatedAt = DateTimeOffset.UtcNow;
            eventRegistration.Status = status;
            await SaveChangesAsync();
        }
    }
}