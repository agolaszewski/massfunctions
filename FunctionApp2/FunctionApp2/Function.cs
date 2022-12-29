using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FunctionApp2
{
    public class Function
    {
        private readonly IMessageReceiver _receiver;
        private readonly IBusControl _bus;

        public Function(IMessageReceiver receiver, IBusControl bus)
        {
            _receiver = receiver;
            _bus = bus;
        }

        [FunctionName("Function")]
        public async Task Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var xd = await _bus.GetSendEndpoint(new Uri("queue:testCommands"));
            await xd.Send<Command>(new Command() { Test = Guid.NewGuid() }, x => x.CorrelationId = Guid.NewGuid());
            await _bus.Publish<EventReceived>(new EventReceived() { Test = Guid.Parse("1fe4852c-086b-4c68-8991-184e67ac9547") });
            await _bus.Publish<EventReceived2>(new EventReceived2() { Test = Guid.Parse("1fe4852c-086b-4c68-8991-184e67ac9547") });
        }
    }
}