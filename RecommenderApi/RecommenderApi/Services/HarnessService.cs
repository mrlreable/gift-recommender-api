using RecommenderApi.Api.ViewModels;
using RecommenderApi.Dtos;

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

        public Task<RecommendationView> GetRecommendationsAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
