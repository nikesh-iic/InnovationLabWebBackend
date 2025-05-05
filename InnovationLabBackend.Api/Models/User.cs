using Microsoft.AspNetCore.Identity;

namespace InnovationLabBackend.Api.Models
{

    public sealed class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}