using personal_trainer_api.Data;
using personal_trainer_api.Services.LoggingService;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace personal_trainer_api.Services.HealthService
{
    public class HealthService(ILoggingService loggingService, IConfiguration configuration) : IHealthCheck
    {
        private readonly ILoggingService _loggingService = loggingService;
        private readonly IConfiguration _configuration = configuration;

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            _loggingService.LogTrace("Health Check Succeeded");

            return HealthCheckResult.Healthy("A healthy result.");
        }

    }
}