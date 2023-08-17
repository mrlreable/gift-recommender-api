namespace RecommenderApi.Options
{
    public class DatabaseConfigurationOption
    {
        public const string SectionName = "GiftRecommenderDatabase";
        public required string ConnectionString { get; init; }
        public required string DatabaseName { get; init; }
        public required string CollectionName { get; init; }
    }
}
