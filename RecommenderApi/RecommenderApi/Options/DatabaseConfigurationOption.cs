namespace RecommenderApi.Options
{
    public class DatabaseConfigurationOption
    {
        public const string SectionName = "GiftRecommenderDatabase";
        public required string ConnectionString { get; init; }
        public required string DatabaseName { get; init; }
        public required string CollectionName { get; init; }
    }

    public class GiftRecommenderDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}
