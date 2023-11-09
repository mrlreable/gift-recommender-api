using Microsoft.Extensions.Options;
using RecommenderApi.Api.Dtos;
using RecommenderApi.Api.ViewModels;
using RecommenderApi.Dtos;
using RecommenderApi.Options;
using System.Text;
using System.Text.Json;

namespace RecommenderApi.Services
{
    public class HarnessService : IHarnessService
    {
        private readonly ILogger<HarnessService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _harnessBaseUrl;

        public HarnessService(ILogger<HarnessService> logger, IHttpClientFactory httpClientFactory, IOptions<UrConfigurationOption> urConfig)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _harnessBaseUrl = $"{urConfig.Value.HarnessUrl}/engines/{urConfig.Value.EngineId}";
        }

        public async Task<HarnessInputResponse> GenerateHarnessInputAsync(UrInputDto inputDto)
        {
            // TODO:
            // - define Harness event request from the UrInputDto
            // - raise multiple HTTP requests for every event in the UrInputDto
            // - run the requests concurrently
            // - give response for only the whole process
            // - if any errors occured, append it to the response

            var client = _httpClientFactory.CreateClient();
            var body = new { };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_harnessBaseUrl + "/events", content);

            response.EnsureSuccessStatusCode();

            var stringRecommendations = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<HarnessInputResponse>(stringRecommendations)!;
        }

        public async Task<RecommendationView?> ItemBasedQueryAsync(string itemId)
        {
            var client = _httpClientFactory.CreateClient();
            var requestUri = _harnessBaseUrl + "/queries";
            var body = new
            {
                Item = itemId,
            };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUri, content);

            response.EnsureSuccessStatusCode();

            var stringRecommendations = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<RecommendationView?>(stringRecommendations);
        }

        public async Task<RecommendationView?> ItemSetBasedQueryAsync(string[] items)
        {
            var client = _httpClientFactory.CreateClient();
            var requestUri = _harnessBaseUrl + "/queries";
            var body = new
            {
                ItemSet = items,
            };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUri, content);

            response.EnsureSuccessStatusCode();

            var stringRecommendations = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<RecommendationView?>(stringRecommendations);
        }

        public async Task<RecommendationView?> UserBasedQueryAsync(string userId)
        {
            var client = _httpClientFactory.CreateClient();
            var requestUri = _harnessBaseUrl + "/queries";
            var body = new
            {
                User = userId,
            };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUri, content);

            response.EnsureSuccessStatusCode();

            var stringRecommendations = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<RecommendationView?>(stringRecommendations);
        }
    }
}
