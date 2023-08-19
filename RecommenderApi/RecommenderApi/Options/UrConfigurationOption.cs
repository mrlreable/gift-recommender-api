namespace RecommenderApi.Options
{
    public class UrConfigurationOption
    {
        public const string SectionName = "UrConfiguration";
        public required string EngineId { get; init; }
        public required string HarnessUrl { get; init; }
    }
}
