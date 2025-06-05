using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rentora.Persistence.Data.DbContext;

namespace Rentora.Persistence.BackgroundServices
{
    internal class OtpCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OtpCleanupService> _logger;
        private readonly TimeSpan _cleanupInterval = TimeSpan.FromMinutes(10);

        public OtpCleanupService(IServiceScopeFactory scopeFactory, ILogger<OtpCleanupService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("OTP Cleanup Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                        var expiredOtps = dbContext.OTPs.Where(o => o.ExpiryTime < DateTime.UtcNow).ToList();

                        if (expiredOtps.Count > 0)
                        {
                            dbContext.OTPs.RemoveRange(expiredOtps);
                            await dbContext.SaveChangesAsync(stoppingToken);

                            _logger.LogInformation($"Deleted {expiredOtps.Count} expired OTPs.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while cleaning up expired OTPs.");
                }

                await Task.Delay(_cleanupInterval, stoppingToken);
            }

            _logger.LogInformation("OTP Cleanup Service is stopping.");
        }
    }
}
