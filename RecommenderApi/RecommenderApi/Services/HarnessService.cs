using RecommenderApi.Api.ViewModels;
using RecommenderApi.Dtos;
using System.Text;
using System.Text.Json;

namespace RecommenderApi.Services
{
    public class HarnessService : IHarnessService
    {
        private readonly ILogger<HarnessService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HarnessService(ILogger<HarnessService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public Task GenerateHarnessInputAsync(UrInputDto inputDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<RecommendationView>?> UserBasedQueryAsync(string userId)
        {
            var client = _httpClientFactory.CreateClient();
            var requestUri = "";
            var body = new
            {
                UserId = userId,
            };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUri, content);

            response.EnsureSuccessStatusCode();

            var stringRecommendations = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ICollection<RecommendationView>?>(stringRecommendations);
        }
    }
}
