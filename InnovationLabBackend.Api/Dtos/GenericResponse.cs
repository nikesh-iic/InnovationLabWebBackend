using System.Net;

namespace InnovationLabBackend.Api.Dtos
{
    public class GenericResponse<T>
    {
        public required HttpStatusCode StatusCode { get; set; }
        public required string Message { get; set; }
        public T? Data { get; set; }
    }
}