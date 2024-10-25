global using template_api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.Filters;
using template_api.Data;
using Newtonsoft.Json;
using template_api.Services.HealthService;
using template_api.Services.UserService;
using template_api.Services.LoggingService;
using template_api.DataAccess.Users;
using Azure.Storage.Blobs;
using Azure.Identity;
using template_api.Services.BlobStorageService;
using template_api.DataAccess.Clients;
using template_api.Services.UserManagementService;
using template_api.DataAccess.UserManagement;
using template_api.Services.TrainerService;
using template_api.DataAccess.Trainers;
using template_api.Services.ClientService;
using template_api.Services.WorkoutService;
using template_api.DataAccess.Workouts;

var builder = WebApplication.CreateBuilder(args);
var configBuilder = new ConfigurationBuilder();
var services = builder.Services;
var allowMyOrigins = "AllowMyOrigins";

builder.Logging.ClearProviders();

var azureConnectionString = Environment.GetEnvironmentVariable("AzureAppConfigEndpoint");
configBuilder.AddAzureAppConfiguration("Endpoint=https://personal-trainer-config.azconfig.io;Id=hMvS;Secret=GEjztpCafAu8B0c0pvnvzK6HaP7zpYcVi2KqusT55V6aYt9MmoS3JQQJ99AHAC1i4TkJ0deGAAACAZACXqaU");


var configuration = configBuilder.Build();


services.AddSingleton<IConfiguration>(configuration);

// Connect to storage account
var blobServiceClient = new BlobServiceClient(
        new Uri(configuration["BlobStorage:BlobServiceEndpoint"]),
        new DefaultAzureCredential());

services.AddSingleton(blobServiceClient);


// Services
services.AddHttpClient();

// Add logging
builder.Logging.AddConsole();
builder.Logging.AddEventSourceLogger();

// Add services to the container.
services.AddDbContext<DataContext>(
    options =>
    {
        options.UseSqlServer(configuration["DbConnectionString"]);
    },
        ServiceLifetime.Transient
);

services.AddControllers();

// Turn off claim mapping for Microsoft middleware
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition(
        "oauth2",
        new OpenApiSecurityScheme
        {
            Description =
                "Standard Authorization header using the Bearer scheme, e.g. \"bearer {token} \"",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        }
    );
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});
services.AddSwaggerGenNewtonsoftSupport();
services.AddAutoMapper(typeof(Program).Assembly);

// Services
services.AddScoped<IUserService, UserService>();
services.AddScoped<IUserManagementService, UserManagementService>();
services.AddScoped<IBlobStorageService, BlobStorageService>();
services.AddScoped<ITrainerService, TrainerService>();
services.AddScoped<IClientService, ClientService>();
services.AddScoped<IWorkoutService, WorkoutService>();

// Data Access
services.AddScoped<IUserDataAccess, UserDataAccess>();
services.AddScoped<IUserManagementDataAccess, UserManagementDataAccess>();
services.AddScoped<IClientDataAccess, ClientDataAccess>();
services.AddScoped<ITrainerDataAccess, TrainerDataAccess>();
services.AddScoped<IWorkoutDataAccess, WorkoutDataAccess>();


// Logging
services.AddTransient<ILoggingService, LoggingService>();

// Authentication
services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(configuration["Key"])
            ),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


services.AddHttpContextAccessor();
services.AddSingleton<IPrincipal>(
    provider => provider.GetService<IHttpContextAccessor>().HttpContext.User
);


services.AddCors(options =>
{
    options.AddPolicy(
        allowMyOrigins,
        builder =>
        {
            builder
                .WithOrigins("http://localhost:3000", "https://localhost:3000", "http://localhost:5000", "https://localhost:5000", "https://financing-app.azurewebsites.net", "https://money-clarity.azurewebsites.net", "https://finance-management.azurewebsites.net")
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

// Health check to database
services.AddHealthChecks()
.AddCheck<HealthService>("HealthCheck", failureStatus: HealthStatus.Degraded)
.AddDbContextCheck<DataContext>();

var app = builder.Build();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
        [HealthStatus.Unhealthy] = StatusCodes.Status500InternalServerError
    },
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var response = new HealthCheckResponse
        {
            Status = report.Status.ToString(),
            HealthChecks = report.Entries.Select(x => new IndividualHealthCheckResponse
            {
                Component = x.Key,
                Status = x.Value.Status.ToString()
            }),
            HealthCheckDuration = report.TotalDuration
        };
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowMyOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
