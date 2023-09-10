
using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
ISubscriber sub = redis.GetSubscriber();
sub.Subscribe("sms", (channel, message) =>
{
    Console.WriteLine($"Message: {message}");
});
Console.ReadKey();
