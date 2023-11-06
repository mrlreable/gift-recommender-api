using RecommenderApi.Api.ViewModels;
using RecommenderApi.Dtos;

namespace RecommenderApi.Services
{
    public interface IHarnessService
    {
        public Task GenerateHarnessInputAsync(UrInputDto inputDto);
        public Task<RecommendationView?> ItemBasedQueryAsync(string itemId);
        public Task<RecommendationView?> UserBasedQueryAsync(string userId);
        public Task<RecommendationView?> ItemSetBasedQueryAsync(string[] items);
    }
}