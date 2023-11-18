using Microsoft.Extensions.Options;
using RecommenderApi.Api.Dtos;
using RecommenderApi.Api.ViewModels;
using RecommenderApi.Dtos;
using RecommenderApi.Options;
using System.Net;
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

        public async Task<EventResponse> GenerateHarnessInputAsync(UrInputDto inputDto)
        {
            var client = _httpClientFactory.CreateClient();

            var requests = CreateHarnessRequests(inputDto);
            var eventResponse = new EventResponse();
            eventResponse.Errors = new Dictionary<string, string[]>();
            eventResponse.StatusCode = HttpStatusCode.OK;

            foreach (var content in requests)
            {
                content.Headers.Add("X-Request-Id", Guid.NewGuid().ToString());
                var response = await client.PostAsync(_harnessBaseUrl + "/events", content);
                var body = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode != HttpStatusCode.Created)
                {
                    eventResponse.StatusCode = HttpStatusCode.MultiStatus;
                    eventResponse.Message = "One or more errors were thrown by Harness Server";

                    eventResponse.Errors.Add(content.Headers.GetValues("X-Request-Id").First(), new[]
                    {
                        response.StatusCode.ToString(),
                        response?.ReasonPhrase ?? "None",
                        body
                    });
                }
                else
                {
                    eventResponse.Message = body;
                }
            }

            //response.EnsureSuccessStatusCode();
            return eventResponse;
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

            return await response.Content.ReadFromJsonAsync<RecommendationView>();
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

            return await response.Content.ReadFromJsonAsync<RecommendationView>();
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

            return await response.Content.ReadFromJsonAsync<RecommendationView>();
        }

        private List<StringContent> CreateHarnessRequests(UrInputDto inputDto)
        {
            return new List<StringContent>
            {
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "sex",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.Sex,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "birthdate",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.BirthDate,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "education",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.Education,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "personality",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.PersonalityType,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "decision_making",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.DecisionMaking,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "information_retrieval",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.InformationRetrieval,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "taste",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.Taste,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "hot_drink",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.HotDrink,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "alcohol_preference",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.AlcoholPreference,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "choc_or_vanil",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.ChocolateOrVanilla,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "birthdate",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.BirthDate,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "coke_or_pepsi",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.CokeOrPepsi,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "jewel",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.Jewel,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "black_or_white",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.BlackOrWhite,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "blue_or_green",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.BlueOrGreen,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "pink_or_purple",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.PinkOrPurple,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "fav_car_brand",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.FavouriteCarBrand,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "fav_season",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.FavouriteSeason,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "cold_or_hot",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.ColdOrHot,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "run_or_cycle",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.RunningOrCycling,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "bus_or_train",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.BusOrTrain,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "moz_or_beth",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.MozartOrBethoven,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "cat_or_dog",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.CatOrDog,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "active_or_relax",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.ActiveOrRelax,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "hike_or_sightsee",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.HikingOrSightseeing,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "city_or_village",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.CityOrVillage,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "plain_or_mountains",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.PlainOrMountains,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "theatre_or_cinema",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.TheatreOrCinema,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "book_or_movie",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.BookOrMovie,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "photo_or_paint",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.PhotoOrPainting,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "t_or_shirt",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.ShirtOrTShirt,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "trad_or_prog",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.TraditionOrProgression,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "email_or_phone",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.EmailOrPhone,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
                new StringContent(JsonSerializer.Serialize(new HarnessInputRequest
                {
                    Event = "folklore_or_industry_art",
                    EntityType = "user",
                    EntityId = inputDto.UserId,
                    TargetEntityType = "item",
                    TargetEntityId = inputDto.FolkloreOrIndustryArt,
                    EventTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                }), Encoding.UTF8, "application/json"),
            };
        }
    }
}
