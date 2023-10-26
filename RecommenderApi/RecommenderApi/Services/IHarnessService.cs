using RecommenderApi.Api.ViewModels;
using RecommenderApi.Dtos;

namespace RecommenderApi.Services
{
    public interface IHarnessService
    {
        public Task GenerateHarnessInputAsync(UrInputDto inputDto);
        public Task<RecommendationView> GetRecommendationsAsync(string userId);
    }
}