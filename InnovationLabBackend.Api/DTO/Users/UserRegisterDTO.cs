using System.ComponentModel.DataAnnotations;

namespace InnovationLabBackend.Api.DTO.Users
{
    public class UserRegisterDTO
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
