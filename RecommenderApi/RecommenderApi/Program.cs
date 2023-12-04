using RecommenderApi.Extensions;
using RecommenderApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.RegisterServices();
builder.ConfigureMapster();
builder.Services.AddHttpClient();

// Health checks
builder.HealthCheck();

builder.ConfigureDatabase();
builder.ConfigureLogger();
builder.ConfigureJson();
builder.ValidateOptions();

var app = builder.Build();

app.SeedMongo();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Disable for now
//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPolicy");
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
