using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestAspNetCore.BackgroundTask
{
    public class RedisSubscriber : BackgroundService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisSubscriber(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscriber = _connectionMultiplexer.GetSubscriber();
            return subscriber.SubscribeAsync("messages", ((channel, value) =>
            {
                Console.WriteLine($"This message content was: {value}");
            }));
        }
    }
}
