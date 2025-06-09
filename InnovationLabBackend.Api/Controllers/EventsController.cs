using System.Net;
using AutoMapper;
using InnovationLabBackend.Api.Dtos;
using InnovationLabBackend.Api.Dtos.EventAgendas;
using InnovationLabBackend.Api.Dtos.EventRegistrations;
using InnovationLabBackend.Api.Dtos.Events;
using InnovationLabBackend.Api.Enums;
using InnovationLabBackend.Api.Interfaces;
using InnovationLabBackend.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnovationLabBackend.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class EventsController(IEventsRepo eventsRepo, IMapper mapper, IMediaService mediaService) : ControllerBase
    {
        private readonly IEventsRepo _eventsRepo = eventsRepo;
        private readonly IMapper _mapper = mapper;
        private readonly IMediaService _mediaService = mediaService;

        [HttpGet(Name = "GetEvents")]
        public async Task<ActionResult<List<EventResponseDto>>> GetEvents([FromQuery] EventFilterDto filters)
        {
            var events = await _eventsRepo.GetEventsAsync(filters);
            var eventsDto = _mapper.Map<List<EventResponseDto>>(events);
            return Ok(eventsDto);
        }

        [HttpGet("{id}", Name = "GetEventById")]
        public async Task<ActionResult<GenericResponse<EventResponseDto>>> GetEventById(Guid id)
        {
            var ev = await _eventsRepo.GetEventByIdAsync(id);

            if (ev == null)
            {
                return NotFound(new GenericResponse<EventResponseDto>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Event not found",
                    Data = null
                });
            }

            var eventDto = _mapper.Map<EventResponseDto>(ev);
            return Ok(new GenericResponse<EventResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success",
                Data = eventDto
            });
        }

        [Authorize]
        [Consumes("multipart/form-data")]
        [HttpPost(Name = "CreateEvent")]
        public async Task<ActionResult<EventResponseDto>> CreateEvent([FromForm] CreateEventDto eventCreateDto)
        {
            if (eventCreateDto.CoverImage == null)
            {
                return BadRequest("Cover image is required.");
            }

            string mediaTypeString = eventCreateDto.CoverImage.ContentType.ToLower();
            MediaType mediaType = mediaTypeString.StartsWith("image") ? MediaType.Image
                : mediaTypeString.StartsWith("video") ? MediaType.Video
                : MediaType.NotSupported;

            if (mediaType == MediaType.NotSupported)
            {
                return StatusCode(
                    StatusCodes.Status415UnsupportedMediaType,
                    "Unsupported file format. Only image and video files are supported."
                );
            }

            string? imageUrl = await _mediaService.UploadAsync(
                file: eventCreateDto.CoverImage,
                mediaType: mediaType,
                folder: "events"
            );

            if (imageUrl == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while uploading the media.");
            }

            var newEvent = _mapper.Map<Event>(eventCreateDto);
            newEvent.CoverImageUrl = imageUrl;

            var createdEvent = await _eventsRepo.CreateEventAsync(newEvent);
            var createdEventDto = _mapper.Map<EventResponseDto>(createdEvent);

            return CreatedAtAction(nameof(GetEvents), new { id = createdEvent.Id }, createdEventDto);
        }

        [Authorize]
        [Consumes("multipart/form-data")]
        [HttpPut("{id}", Name = "UpdateEvent")]
        public async Task<ActionResult> UpdateEvent(Guid id, [FromForm] UpdateEventDto eventUpdateDto)
        {
            if (eventUpdateDto == null)
            {
                return BadRequest("Invalid data");
            }

            var ev = await _eventsRepo.GetEventByIdAsync(id);

            if (ev == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(eventUpdateDto, ev);

            // Handle CoverImage update
            if (eventUpdateDto.CoverImage != null)
            {
                string mediaTypeString = eventUpdateDto.CoverImage.ContentType.ToLower();
                MediaType mediaType = mediaTypeString.StartsWith("image") ? MediaType.Image
                    : mediaTypeString.StartsWith("video") ? MediaType.Video
                    : MediaType.NotSupported;

                if (mediaType == MediaType.NotSupported)
                {
                    return StatusCode(
                        StatusCodes.Status415UnsupportedMediaType,
                        "Unsupported file format. Only image and video files are supported."
                    );
                }

                string? imageUrl = await _mediaService.UploadAsync(
                    file: eventUpdateDto.CoverImage,
                    mediaType: mediaType,
                    folder: "events"
                );

                if (imageUrl == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error while uploading the media.");
                }

                ev.CoverImageUrl = imageUrl;
            }
            // else: do not change CoverImageUrl

            await _eventsRepo.UpdateEventAsync(ev);

            return NoContent();
        }

        [HttpGet("{id}/agenda", Name = "GetEventAgenda")]
        public async Task<ActionResult<GenericResponse<List<EventAgendaResponseDto>>>> GetEventAgenda(Guid id)
        {
            var agenda = await _eventsRepo.GetEventAgendaAsync(id);
            var agendaDto = _mapper.Map<List<EventAgendaResponseDto>>(agenda);

            return Ok(new GenericResponse<List<EventAgendaResponseDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Event agenda retrieved successfully",
                Data = agendaDto
            });
        }

        [Authorize]
        [Consumes("application/json")]
        [HttpPost("{id}/agenda", Name = "CreateEventAgenda")]
        public async Task<ActionResult<GenericResponse<EventAgendaResponseDto>>> CreateEventAgenda(Guid id, [FromBody] CreateEventAgendaDto agendaDto)
        {
            var ev = await _eventsRepo.GetEventByIdAsync(id);

            if (ev == null)
            {
                return NotFound(new GenericResponse<EventAgendaResponseDto>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Event not found"
                });
            }

            var newAgenda = _mapper.Map<EventAgenda>(agendaDto);
            newAgenda.EventId = id;

            var createdAgenda = await _eventsRepo.CreateEventAgendaAsync(newAgenda);
            return CreatedAtAction(nameof(GetEventAgenda), new { id = createdAgenda.EventId }, createdAgenda);
        }

        [Authorize]
        [Consumes("application/json")]
        [HttpPut("/agenda/{agendaId}", Name = "UpdateEventAgenda")]
        public async Task<ActionResult<GenericResponse>> UpdateEventAgenda(Guid agendaId, [FromBody] UpdateEventAgendaDto updateEventAgendaDto)
        {
            var agenda = await _eventsRepo.GetEventAgendaByIdAsync(agendaId);

            if (agenda == null)
            {
                return NotFound(new GenericResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Event agenda not found"
                });
            }

            var agendaToUpdate = _mapper.Map<EventAgenda>(updateEventAgendaDto);
            await _eventsRepo.UpdateEventAgendaAsync(agendaToUpdate);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("/agenda/{agendaId}", Name = "DeleteEventAgenda")]
        public async Task<ActionResult<GenericResponse>> DeleteEventAgenda(Guid agendaId)
        {
            var agenda = await _eventsRepo.GetEventAgendaByIdAsync(agendaId);

            if (agenda == null)
            {
                return NotFound(new GenericResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Event agenda not found"
                });
            }

            var agendaToDelete = _mapper.Map<EventAgenda>(agenda);
            await _eventsRepo.DeleteEventAgendaAsync(agendaToDelete);

            return NoContent();
        }

        [Consumes("application/json")]
        [HttpPost("{id}/register", Name = "RegisterForEvent")]
        public async Task<ActionResult<EventRegistrationResponseDto>> RegisterForEvent(Guid id, [FromBody] EventRegistrationDto registrationDto)
        {
            var ev = await _eventsRepo.GetEventByIdAsync(id);

            if (ev == null)
            {
                return NotFound(new
                {
                    Message = "Event Not Found"
                });
            }

            if (ev.IsTeamEvent)
            {
                if (registrationDto.TeamMembers == null || registrationDto.TeamMembers.Count == 0)
                {
                    return BadRequest(new
                    {
                        Message = "Team Members are required for team event"
                    });
                }

                if (registrationDto.TeamMembers.Count > ev.MaxTeamMembers)
                {
                    return BadRequest(new
                    {
                        Message = $"Maximum of {ev.MaxTeamMembers} team members are only allowed"
                    });
                }
            }
            else
            {
                if (registrationDto.TeamMembers != null && registrationDto.TeamMembers.Count != 0)
                {
                    return BadRequest(new
                    {
                        Message = "Team Members are not allowed for solo event"
                    });
                }
            }

            var newRegistration = new EventRegistration
            {
                EventId = id,
                Type = ev.IsTeamEvent ? EventRegistrationType.Team : EventRegistrationType.Solo,
                Name = registrationDto.Name,
                Email = registrationDto.Email,
                Phone = registrationDto.Phone,
                Status = EventRegistrationStatus.Pending,
            };

            var createdRegistration = await _eventsRepo.RegisterEventAsync(newRegistration);
            return CreatedAtAction(nameof(GetEventRegistrations), new { id = createdRegistration.EventId }, createdRegistration);
        }

        [Authorize]
        [Consumes("application/json")]
        [HttpGet("{id}/registrations", Name = "GetEventRegistrations")]
        public async Task<ActionResult<List<EventRegistrationResponseDto>>> GetEventRegistrations(Guid id, [FromQuery] EventRegistrationFilterDto filters)
        {
            var registrations = await _eventsRepo.GetEventRegistrationsAsync(id, filters);
            var registrationsDto = _mapper.Map<List<EventRegistrationResponseDto>>(registrations);
            return Ok(registrationsDto);
        }

        [Authorize]
        [Consumes("application/json")]
        [HttpPatch("/registrations/{registrationId}/status", Name = "UpdateEventRegistrationStatus")]
        public async Task<ActionResult> UpdateEventRegistrationStatus(Guid registrationId, [FromBody] UpdateEventRegistrationDto updateEventRegistrationDto)
        {
            var registration = await _eventsRepo.GetEventRegistrationByIdAsync(registrationId);

            if (registration == null)
            {
                return NotFound();
            }

            await _eventsRepo.UpdateEventRegistrationStatusAsync(registration, updateEventRegistrationDto.Status);
            return NoContent();
        }
    }
}