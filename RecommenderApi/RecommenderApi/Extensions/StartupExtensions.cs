using FluentValidation;
using RecommenderApi.Options;
using RecommenderApi.Options.Validators;
using RecommenderApi.Services;
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
            builder.Services.AddSingleton<IValidator<UrConfigurationOption>, UrOptionsValidator>();
            builder.Services.AddSingleton<IValidator<DatabaseConfigurationOption>, DatabaseConfigurationOptionValidator>();

            builder.Services.AddOptions<DatabaseConfigurationOption>()
                .Bind(builder.Configuration.GetSection(DatabaseConfigurationOption.SectionName))
                .ValidateFluently()
                .ValidateOnStart();

            builder.Services.AddOptions<UrConfigurationOption>()
                .Bind(builder.Configuration.GetSection(UrConfigurationOption.SectionName))
                .ValidateFluently()
                .ValidateOnStart();
        }

        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IHarnessService, HarnessService>();
        }

        public static void HealthCheck(this WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks();
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
