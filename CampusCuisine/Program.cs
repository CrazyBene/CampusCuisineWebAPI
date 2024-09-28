using CampusCuisine.Data;
using CampusCuisine.Middlewares;
using CampusCuisine.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddSingleton<Mapper>();
builder.Services.AddScoped<RecipeService>();
builder.Services.AddScoped<RatingsService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention());

var app = builder.Build();

app.UseExceptionHandler();
app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();

app.Run();
