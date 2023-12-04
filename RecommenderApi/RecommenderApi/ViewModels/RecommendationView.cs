namespace RecommenderApi.Api.ViewModels
{
    public class RecommendationView
    {
        public List<ResultItem> Result { get; set; }
    }

    public class ResultItem
    {
        public string Item { get; set; }
        public double Score { get; set; }
    }
}
