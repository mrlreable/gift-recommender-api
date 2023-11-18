using RecommenderApi.Extensions;
using RecommenderApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.RegisterServices();
builder.ConfigureMapster();
builder.Services.AddHttpClient();
builder.Services.AddControllers();

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
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
