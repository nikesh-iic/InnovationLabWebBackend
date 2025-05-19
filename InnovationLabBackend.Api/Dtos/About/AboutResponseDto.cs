﻿﻿using AutoMapper;
using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Dtos.About
{
    [AutoMap(typeof(Models.About), ReverseMap = true)]
    public class AboutResponseDto
    {
        public Guid Id { get; set; }
        
        // Mission Vision
        public string? Mission { get; set; }
        public string? Vision { get; set; }
        
        // Parent Organization
        public string? ParentOrgName { get; set; }
        public string? ParentOrgDescription { get; set; }
        public string? ParentOrgLogoUrl { get; set; }
        public string? ParentOrgWebsiteUrl { get; set; }
        
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
