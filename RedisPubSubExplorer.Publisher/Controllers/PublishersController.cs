using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisPubSubExplorer.Publisher.Models;
using StackExchange.Redis;

namespace RedisPubSubExplorer.Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        IConfiguration _configuration;
        public PublishersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("publisher")]
        public async Task<IActionResult> Publisher(Content message)
        {
            ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(_configuration.GetSection("RedisConf").Value);
            ISubscriber subscriber = connectionMultiplexer.GetSubscriber();
            await subscriber.PublishAsync("sms", message.Message);
            return Ok(new { success = true, data = message });
        }
    }
}
