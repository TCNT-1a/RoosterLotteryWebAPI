using Microsoft.EntityFrameworkCore;
using RoosterLotteryWebAPI.Batch;
using Service.Models;
using Quartz;
using IJob = Quartz.IJob;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env}.json", optional: false)
    .Build();

builder.Services.AddSingleton<IConfigurationRoot>(configuration);

string connectionString = configuration.GetConnectionString("DbConnection") ?? "";
if (connectionString == null)
{
    throw new InvalidOperationException("DbConnection connection string is missing");
}
// builder.Services.AddHostedService<CreatingInitialBet>();
builder.Services.AddControllers();

builder.Services.AddDbContext<RoosterLotteryContext>((options) => options.UseSqlServer(connectionString));
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    string cronH = "0 0 1 * * ?";
    string cronM = "0 * * * * ?";

    q.ScheduleJob<MyJob>(trigger => trigger
        .WithIdentity("my-job-trigger", "default")
        .WithCronSchedule(cronM)
    );
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var schedulerFactory = scope.ServiceProvider.GetRequiredService<ISchedulerFactory>();
    var scheduler = schedulerFactory.GetScheduler().Result;
    scheduler.Start().Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
