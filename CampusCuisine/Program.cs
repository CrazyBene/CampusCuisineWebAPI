using CampusCuisine.Data;
using CampusCuisine.Middlewares;
using CampusCuisine.Services;
using Keycloak.AuthServices.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<Mapper>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<RecipeService>();
builder.Services.AddScoped<RatingsService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention());

builder.Services.AddKeycloakWebApiAuthentication(configuration);

var app = builder.Build();

app.UseExceptionHandler();
app.UseMiddleware<LoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }