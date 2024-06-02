using Microsoft.EntityFrameworkCore;
using Service.Models;

namespace RoosterLotteryWebAPI.Batch
{
  
    public class CreatingInitialBet : IHostedService
    {
        private readonly ILogger<CreatingInitialBet> _logger;
        private readonly IConfiguration _configuration;

        public CreatingInitialBet(ILogger<CreatingInitialBet> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Running background task...");

                var optionsBuilder = new DbContextOptionsBuilder<RoosterLotteryContext>();
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DbConnection"));

                using (var _context = new RoosterLotteryContext(optionsBuilder.Options))
                {
                    var c = _context.Database
                    .ExecuteSqlRaw("EXEC dbo.CreateInitialBet");
                    Console.WriteLine("doing task");
                    // Do your background task here
                }


                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }

        public Task DisposeAsync(CancellationToken cancellationToken)
        {
            // Dispose of any resources here
            _logger.LogInformation("Running CreatingInitialBet background task...");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
