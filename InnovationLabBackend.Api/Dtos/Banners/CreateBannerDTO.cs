using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace InnovationLabBackend.Api.Dtos.Banner
{
    [AutoMap(typeof(Models.Banner), ReverseMap = true)]
    public class CreateBannerDTO
    {
        [Required]
        public required IFormFile Url { get; set; }
        [Required]
        public Enums.BannerType Type { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string SubTitle { get; set; } = string.Empty;
        [Required]
        public string Caption { get; set; } = string.Empty;
        public bool Current { get; set; }
        [Range(0, int.MaxValue)]

        public int Version { get; set; }
        public string? ParentId { get; set; }
        public DateTimeOffset? ScheduledStart { get; set; }
        public DateTimeOffset? ScheduledEnd { get; set; }
    }
}
