using RecommenderApi.Options;
using Serilog;
using System.Text.Json.Serialization;

namespace RecommenderApi.Extensions
{
    public static class StartupExtensions
    {
        public static void ConfigureLogger(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Logging.AddSerilog(logger);
        }

        public static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<GiftRecommenderDatabaseSettings>(
                builder.Configuration.GetSection("GiftRecommenderDatabase"));
        }

        public static void ValidateOptions(this WebApplicationBuilder builder)
        {
            builder.Services.AddOptions<DatabaseConfigurationOption>()
                .Bind(builder.Configuration.GetSection(DatabaseConfigurationOption.SectionName))
                .ValidateOnStart();

            builder.Services.AddOptions<UrConfigurationOption>()
                .Bind(builder.Configuration.GetSection(UrConfigurationOption.SectionName))
                .ValidateOnStart()
                .ValidateFluently();
        }

        public static void ConfigureJson(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }
    }
}
