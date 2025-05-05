using System.ComponentModel.DataAnnotations;

namespace InnovationLabBackend.Api.DTO.Users
{
    public class UserVerifyDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Otp {get; set;}
    }
}
