using RecommenderApi.Dtos;

namespace RecommenderApi.Services
{
    public class HarnessInputService : IHarnessService
    {
        private readonly ILogger<HarnessInputService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HarnessInputService(ILogger<HarnessInputService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task GenerateHarnessInput(UrInputDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
