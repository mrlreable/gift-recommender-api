namespace RecommenderApi.Api.ViewModels
{
    public class RecommendationView
    {
        public Result[] Result { get; set; }
    }

    public class Result
    {
        public string Item { get; set; }
        public double Score { get; set; }
    }
}
