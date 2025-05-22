﻿using System.ComponentModel.DataAnnotations;

namespace InnovationLabBackend.Api.Models
{
    public class About
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Mission Vision
        public string? Mission { get; set; }
        public string? Vision { get; set; }

        // Parent Organization
        public string? ParentOrgName { get; set; }
        public string? ParentOrgDescription { get; set; }
        public string? ParentOrgLogoUrl { get; set; }
        public string? ParentOrgWebsiteUrl { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset DeletedAt { get; set; }
    }
}
