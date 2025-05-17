using System.ComponentModel.DataAnnotations;
using AutoMapper;
using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Dtos.Events
{
    [AutoMap(typeof(EventRegistration), ReverseMap = true)]
    public class EventRegistrationDto : TeamMemberDto
    {
        public List<TeamMemberDto>? TeamMembers { get; set; }
    }

    [AutoMap(typeof(TeamMember), ReverseMap = true)]
    public class TeamMemberDto
    {
        [Required]
        public required string Name { get; set; }
        [Required, EmailAddress]
        public required string Email { get; set; }
        [Phone]
        public string? Phone { get; set; }
    }
}