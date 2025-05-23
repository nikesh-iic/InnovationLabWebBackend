using System.ComponentModel.DataAnnotations;
using InnovationLabBackend.Api.Enums;

namespace InnovationLabBackend.Api.Dtos.Events
{
    public class UpdateEventRegistrationDto
    {
        [Required]
        public required EventRegistrationStatus Status { get; set; }
    }
}