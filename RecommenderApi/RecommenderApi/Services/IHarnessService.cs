using RecommenderApi.Api.ViewModels;
using RecommenderApi.Dtos;

namespace RecommenderApi.Services
{
    public interface IHarnessService
    {
        public Task GenerateHarnessInputAsync(UrInputDto inputDto);
        public Task<ICollection<RecommendationView>?> UserBasedQueryAsync(string userId);
    }
}