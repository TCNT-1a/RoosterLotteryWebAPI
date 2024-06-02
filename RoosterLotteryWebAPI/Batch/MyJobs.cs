
using Microsoft.EntityFrameworkCore;
using Quartz;
using Service.Models;
using IJob = Quartz.IJob;

namespace RoosterLotteryWebAPI.Batch
{
    public class MyJob : IJob
    {
        private readonly ILogger<MyJob> _logger;
        private readonly IConfiguration _configuration;
        public MyJob(ILogger<MyJob> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RoosterLotteryContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DbConnection"));

            using (var _context = new RoosterLotteryContext(optionsBuilder.Options))
            {

                var c = _context.Database
               .ExecuteSqlRaw("EXEC dbo.PerformLotteryDraw");

                var c1 = _context.Database
                .ExecuteSqlRaw("EXEC dbo.CreateInitialBet");
                _logger.LogInformation("Running background task......................................");
                
                // Do your background task here
            }

            return Task.CompletedTask;
        }
    }
}
