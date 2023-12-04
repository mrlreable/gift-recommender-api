using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using RecommenderApi.Dtos;
using RecommenderApi.Options;
using RecommenderApi.Options.Validators;
using RecommenderApi.Services;
using RecommenderApi.Validation;
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

        public static void ConfigureMapster(this WebApplicationBuilder builder)
        {
            // TODO: add TypeAdapterConfig for mapster

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
            builder.Services.AddScoped<IValidator<UrInputDto>, UrInputValidator>();
        }

        public static void HealthCheck(this WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks();
        }

        public static void SeedMongo(this WebApplication app)
        {
            var dbConfig = app.Configuration.GetSection(DatabaseConfigurationOption.SectionName).Get<DatabaseConfigurationOption>();
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);

            // Seed events
            var collection = database.GetCollection<BsonDocument>("events");
            if (collection.CountDocuments(FilterDefinition<BsonDocument>.Empty) == 0)
            {
                var data = File.ReadAllText(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Seed", $"{dbConfig.DatabaseName}.events.json"));
                var documents = BsonSerializer.Deserialize<BsonDocument[]>(data);
                collection.InsertMany(documents);
            }

            // Seed items
            collection = database.GetCollection<BsonDocument>("items");
            if (collection.CountDocuments(FilterDefinition<BsonDocument>.Empty) == 0)
            {
                var data = File.ReadAllText(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Seed", $"{dbConfig.DatabaseName}.items.json"));
                var documents = BsonSerializer.Deserialize<BsonDocument[]>(data);
                collection.InsertMany(documents);
            }
        }

        public static void ConfigureJson(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }

        public static void ConfigureCors(this WebApplicationBuilder builder)
        {
            string[] origins = builder.Configuration["AllowedHosts"].Split(';')
          .Select(origin => origin.Trim()).ToArray();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(origins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }
    }
}
