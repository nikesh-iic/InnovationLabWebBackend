using System.Text.Json.Serialization;

namespace InnovationLabBackend.Api.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EventSortBy
    {
        Title,
        StartTime,
        EndTime,
        RegistrationStart,
        RegistrationEnd
    }
}