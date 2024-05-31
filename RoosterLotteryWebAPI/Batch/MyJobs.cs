
using Quartz;
using IJob = Quartz.IJob;

namespace RoosterLotteryWebAPI.Batch
{
    public class MyJob : IJob
    {
        private readonly ILogger<MyJob> _logger;
        public MyJob(ILogger<MyJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Thực hiện tác vụ định thời của bạn tại đây
            //Console.WriteLine("Chạy tác vụ định thời vào giây đầu tiên của mỗi giờ");
            _logger.LogInformation("Chạy tác vụ định thời vào giây đầu tiên của mỗi giờ");
            return Task.CompletedTask;
        }
    }
}
