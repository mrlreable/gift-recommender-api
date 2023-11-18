using System.Net;
using System.Text.Json;

namespace RecommenderApi.Api.ViewModels
{
    public class EventResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public IDictionary<string, string[]>? Errors { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
