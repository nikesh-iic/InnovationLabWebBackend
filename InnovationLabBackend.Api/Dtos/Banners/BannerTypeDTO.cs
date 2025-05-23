namespace InnovationLabBackend.Api.Dtos.Banners
{
    public class BannerTypeDTO
    {
        public bool Success { get; set; }
        public string? Url { get; set; }
        public Enums.BannerType MediaType { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
