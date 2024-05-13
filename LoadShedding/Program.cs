using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoadShedding.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddLoadShedding((options) => 
        {
            // https://farfetch.github.io/loadshedding/docs/guides/adaptative-concurreny-limiter/configuration
            // options.AdaptativeLimiter.ConcurrencyOptions.MinQueueSize = 10;
            // options.AdaptativeLimiter.ConcurrencyOptions.MinConcurrencyLimit = 5;
            // options.AdaptativeLimiter.ConcurrencyOptions.InitialConcurrencyLimit = 5;
            // options.AdaptativeLimiter.ConcurrencyOptions.InitialQueueSize = 50;
            // options.AdaptativeLimiter.ConcurrencyOptions.Tolerance = 2;
            // options.AdaptativeLimiter.ConcurrencyOptions.QueueTimeoutInMs = 60000;

            options.AdaptativeLimiter.UseEndpointPriorityResolver();
            options.SubscribeEvents(events => 
            {
                // other available are ItemEnqueued, ItemDequeued, ItemProcessing and ItemProcessed
                events.Rejected.Subscribe(args => Console.Error.WriteLine($"Item rejected with Priority: {args.Priority}"));
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseLoadShedding();
        app.MapControllers();

        app.Run();
    }
}
