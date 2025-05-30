using System.Text.Json.Serialization;

namespace InnovationLabBackend.Api.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EventStatus
    {
        Upcoming,
        Ongoing,
        Past
    }
}
